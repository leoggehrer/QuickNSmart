//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickNSmart.AspMvc.Models.Modules.Export;
using QuickNSmart.AspMvc.Modules.Session;

namespace QuickNSmart.AspMvc.Controllers
{
    public abstract partial class MvcController : Controller
    {
        protected IFactoryWrapper Factory { get; private set; }
        static MvcController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        protected MvcController(IFactoryWrapper factoryWrapper)
        {
            Constructing();
            Factory = factoryWrapper;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        #region SessionWrapper
        public bool IsSessionAvailable => HttpContext?.Session != null;
        private ISessionWrapper sessionWrapper = null;
        internal ISessionWrapper SessionWrapper => sessionWrapper ?? (sessionWrapper = new SessionWrapper(HttpContext.Session));
        #endregion

        protected static M ConvertTo<M, I>(I contract)
            where M : Models.ModelObject, Contracts.ICopyable<I>, new()
        {
            contract.CheckArgument(nameof(contract));

            M result = new M();

            result.CopyProperties(contract);
            return result;
        }

        protected string ControllerName => GetType().Name.Replace("Controller", string.Empty);

        #region Export-Helpers
        protected virtual string Separator => ";";
        protected virtual string CsvNull => "<NULL>";
        protected virtual string[] CsvHeader => new string[] { "Id" };

        protected virtual FileResult ExportDefault(IEnumerable<string> csvHeader, IEnumerable<Object> exportObjects, string fileName)
        {
            csvHeader.CheckArgument(nameof(csvHeader));
            exportObjects.CheckArgument(nameof(exportObjects));
            fileName.CheckArgument(nameof(fileName));

            List<byte> contentData = new List<byte>();
            var encodingPreamble = Encoding.UTF8.GetPreamble();

            contentData.AddRange(encodingPreamble);
            contentData.AddRange(Encoding.UTF8.GetBytes(csvHeader.Aggregate((s1, s2) => $"{s1}{Separator}{s2}")));
            foreach (var item in exportObjects)
            {
                StringBuilder exportLine = new StringBuilder();

                foreach (var field in csvHeader)
                {
                    if (exportLine.Length > 0)
                        exportLine.Append(Separator);

                    var pi = item.GetType().GetProperty(field);

                    if (pi != null && pi.CanRead)
                    {
                        var value = pi.GetValue(item);

                        if (value != null)
                        {
                            exportLine.Append(value.ToString());
                        }
                        else
                        {
                            exportLine.Append(CsvNull);
                        }
                    }
                }
                contentData.AddRange(Encoding.UTF8.GetBytes(Environment.NewLine));
                contentData.AddRange(Encoding.UTF8.GetBytes(exportLine.ToString()));
            }
            string contentType = "text/csv";
            return File(contentData.ToArray(), contentType, fileName);
        }
        protected virtual IEnumerable<ImportModel<T>> ImportDefault<T>(string[] csvHeader)
            where T : new()
        {
            var result = new List<ImportModel<T>>();
            var fileCount = GetRequestFileCount();

            if (fileCount == 1)
            {
                var hpf = GetRequestFileData(0);

                if (hpf.Length > 0)
                {
                    var idIdx = Array.IndexOf(CsvHeader, "Id");
                    var text = Encoding.Default.GetString(hpf, 0, hpf.Length);
                    var lines = text.Split(Environment.NewLine);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        var model = new ImportModel<T>();
                        var data = lines[i].Split(Separator);

                        if (idIdx >= 0 && CsvHeader.Length == data.Length)
                        {
                            if (Int32.TryParse(data[idIdx], out int id))
                            {
                                if (id < 0)
                                {
                                    model.Action = ImportAction.Delete;
                                    model.Id = Math.Abs(id);
                                }
                                else if (id > 0)
                                {
                                    model.Action = ImportAction.Update;
                                    model.Id = id;
                                    model.Model = CreateModelFromCsv<T>(CsvHeader, data);
                                }
                                else
                                {
                                    model.Action = ImportAction.Insert;
                                    model.Id = id;
                                    model.Model = CreateModelFromCsv<T>(CsvHeader, data);
                                }
                            }
                            else
                            {
                                data[idIdx] = "0";
                                model.Action = ImportAction.Insert;
                                model.Id = id;
                                model.Model = CreateModelFromCsv<T>(CsvHeader, data);
                            }
                        }
                        result.Add(model);
                    }
                }
            }
            return result;
        }

        protected virtual T CreateModelFromCsv<T>(string[] header, string[] data)
            where T : new()
        {
            T result = new T();

            for (int i = 0; i < header.Length && i < data.Length; i++)
            {
                var pi = result.GetType().GetProperty(header[i]);

                if (pi != null && pi.CanWrite)
                {
                    var csvVal = data[i];

                    if (csvVal.Equals(CsvNull))
                    {
                        pi.SetValue(result, null);
                    }
                    else if (pi.PropertyType.IsEnum)
                    {
                        pi.SetValue(result, Enum.Parse(pi.PropertyType, csvVal));
                    }
                    else
                    {
                        pi.SetValue(result, Convert.ChangeType(csvVal, pi.PropertyType));
                    }
                }
            }
            return result;
        }
        #endregion Export-Helpers

        #region File-Helpers
        protected int GetRequestFileCount()
        {
            return Request.Form.Files.Count;
        }
        protected IFormFile GetRequestFormFile(int index)
        {
            IFormFile result = null;

            if (Request.Form.Files.Count > index)
            {
                result = Request.Form.Files[index];
            }
            return result;
        }
        protected string GetRequestFileName(int index)
        {
            IFormFile formFile = GetRequestFormFile(index);

            return formFile?.FileName ?? string.Empty;
        }
        protected byte[] GetRequestFileData(int index)
        {
            return GetRequestFileData(GetRequestFormFile(index));
        }
        protected byte[] GetRequestFileData(IFormFile formFile)
        {
            byte[] result = null;

            if (formFile != null)
            {
                using var inputStream = formFile.OpenReadStream();
                if (!(inputStream is MemoryStream memoryStream))
                {
                    using (memoryStream = new MemoryStream())
                    {
                        inputStream.CopyTo(memoryStream);
                        result = memoryStream.ToArray();
                    }
                }
                else
                {
                    result = memoryStream.ToArray();
                }
            }
            return result;
        }
        #endregion File-Helpers

        #region Error-helpers
        protected string GetModelStateError()
        {
            string[] errors = GetModelStateErrors();

            return string.Join($"{Environment.NewLine}", errors);
        }
        protected string[] GetModelStateErrors()
        {
            List<string> list = new List<string>();
            var errorLists = ModelState.Where(x => x.Value.Errors.Count > 0)
                                       .Select(x => new { x.Key, x.Value.Errors });

            foreach (var errorList in errorLists)
            {
                foreach (var error in errorList.Errors)
                {
                    list.Add($"{errorList.Key}: {error.ErrorMessage}");
                }
            }
            return list.ToArray();
        }
        protected static string GetExceptionError(Exception source)
        {
            source.CheckArgument(nameof(source));

            string tab = string.Empty;
            string errMsg = source.Message;
            Exception innerException = source.InnerException;

            while (innerException != null)
            {
                tab += "\t";
                errMsg = $"{errMsg}{Environment.NewLine}{tab}{innerException.Message}";
                innerException = innerException.InnerException;
            }
            return errMsg;
        }
        #endregion Error-Helpers
    }
}
//MdEnd
//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class TransferGenerator
    {
        partial void CreateTransferPropertyAttributes(Type type, string propertyName, List<string> codeLines)
        {
            if (type.Name.Equals("ILoginUser"))
            {
                if (propertyName.Equals("User"))
                {
                    codeLines.Add("[JsonIgnore]");
                }
                else if (propertyName.Equals("Roles"))
                {
                    codeLines.Add("[JsonIgnore]");
                }
            }
        }
    }
}
//MdEnd
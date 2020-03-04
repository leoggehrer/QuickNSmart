//@QnSBaseCode
//MdStart
using System;
using System.Reflection;
using CommonBase.Extensions;
using System.Threading.Tasks;
using QuickNSmart.Logic.DataContext;
using QuickNSmart.Logic.Modules.Security;

namespace QuickNSmart.Logic.Controllers
{
    internal abstract partial class ControllerObject : IDisposable
    {
        static ControllerObject()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        private bool contextDispose;
        protected IContext Context { get; private set; }

        protected event EventHandler ChangedAuthenticationToken;

        private string authenticationToken;

        /// <summary>
        /// Sets the authorization token.
        /// </summary>
        public string AuthenticationToken
        {
            internal get => authenticationToken;
            set
            {
                authenticationToken = value;
                ChangedAuthenticationToken?.Invoke(this, EventArgs.Empty);
            }
        }

        protected ControllerObject(IContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Constructing();
            contextDispose = true;
            Context = context;
            Constructed();
        }
        protected ControllerObject(ControllerObject controller)
        {
            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            Constructing();
            contextDispose = false;
            Context = controller.Context;
            AuthenticationToken = controller.AuthenticationToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        protected virtual void CheckAuthorization(MethodBase methodeBase)
        {
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(methodeBase, ref handled);
            if (handled == false)
            {
                Authorization.CheckAuthorization(AuthenticationToken, methodeBase);
            }
            AfterCheckAuthorization(methodeBase);
        }
        partial void BeforeCheckAuthorization(MethodBase methodeBase, ref bool handled);
        partial void AfterCheckAuthorization(MethodBase methodeBase);

        protected virtual void CheckAuthorization(Type instanceType, MethodBase methodeBase)
        {
            instanceType.CheckArgument(nameof(instanceType));
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(instanceType, methodeBase, ref handled);
            if (handled == false)
            {
                Authorization.CheckAuthorization(AuthenticationToken, instanceType, methodeBase);
            }
            AfterCheckAuthorization(instanceType, methodeBase);
        }
        partial void BeforeCheckAuthorization(Type instanceType, MethodBase methodeBase, ref bool handled);
        partial void AfterCheckAuthorization(Type instanceType, MethodBase methodeBase);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (contextDispose && Context != null)
                    {
                        Context.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                Context = null;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ControllerObject()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
//MdEnd

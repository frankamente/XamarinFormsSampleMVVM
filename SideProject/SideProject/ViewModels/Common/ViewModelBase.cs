using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SideProject.ViewModels.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {

        #region Constructor
        
        #endregion

        #region DisplayName

        /// <summary>
        ///    Returns the user-friendly name of this object.
        ///    Child classes can set this property to a new value,
        ///    or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion

        #region Debugging Aides

        /// <summary>
        ///    Warns the developer if this object does not have
        ///    a public property with the specified name. This
        ///    method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var msg = "Invalid property name: " + propertyName;

                if (ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                Debug.Fail(msg);
            }
        }

        /// <summary>
        ///    Returns whether an exception is thrown, or if a Debug.Fail() is used
        ///    when an invalid property name is passed to the VerifyPropertyName method.
        ///    The default value is false, but subclasses used by unit tests might
        ///    override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        ///    Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///    Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected virtual void NotifyPropertyChangedAll(object inOjbect)
        {
            foreach (var pi in inOjbect.GetType().GetProperties()) NotifyPropertyChanged(pi.Name);
        }

        public virtual void Refresh()
        {
            NotifyPropertyChangedAll(this);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///    Invoked when this object is being removed from the application
        ///    and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        ///    Child classes can override this method to perform
        ///    clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        ///    Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            var msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
            Debug.WriteLine(msg);
        }
#endif

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nuxiba.TestArch.Entities
{
    /// <summary>
    /// The entity.
    /// </summary>
    [Serializable]
    public abstract class Entity
    {
        #region Fields

        /// <summary>
        /// The properties.
        /// </summary>
        readonly Dictionary<string, PropertyInfo> _properties;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Entidad"/> class.
        /// </summary>
        protected Entity()
        {
            _properties = new Dictionary<string, PropertyInfo>();
            GetType().GetProperties().ToList().ForEach(x => _properties.Add(x.Name, x));
        }

        #endregion Constructors

        #region Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object this[string propertyName]
        {
            get { return GetPropertyValue<object>(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Set property value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="propertyValue">
        /// The property value.
        /// </param>
        /// <typeparam name="TProperty">
        /// Propiedad a la que se le asignara un valor
        /// </typeparam>
        /// <exception cref="ArgumentException">
        /// Propiedad no encontrada
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// No se puede asignar a la propiedad
        /// </exception>
        public void SetPropertyValue<TProperty>(string propertyName, TProperty propertyValue)
        {
            if (!_properties.ContainsKey(propertyName)) throw new ArgumentException(string.Format("Propiedad no encontrada: {0}", propertyName));
            if (!_properties[propertyName].CanWrite) throw new InvalidOperationException(string.Format("Imposible asignar propiedad: {0}", propertyName));
            if (!typeof(TProperty).IsAssignableFrom(_properties[propertyName].PropertyType)) throw new ArgumentException(string.Format("Imposible convertir al tipo solicitado la propiedad: {0}", propertyName));

            _properties[propertyName].SetValue(this, propertyValue, null);
        }

        /// <summary>
        /// Get property value.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <typeparam name="TProperty">
        /// Propiedad de la que se obtendrá un valor
        /// </typeparam>
        /// <returns>
        /// The <see cref="TProperty"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Propiedad no encontrada <see cref="TProperty"/>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Imposible leer propiedad <see cref="TProperty"/>
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Imposible convertir al tipo solicitado la propiedad <see cref="TProperty"/>
        /// </exception>
        public TProperty GetPropertyValue<TProperty>(string propertyName)
        {
            if (!_properties.ContainsKey(propertyName)) throw new ArgumentException(string.Format("Propiedad no encontrada: {0}", propertyName));
            if (!_properties[propertyName].CanRead) throw new InvalidOperationException(string.Format("Imposible leer propiedad: {0}", propertyName));

            var propertyValue = _properties[propertyName].GetValue(this, null);
            if (!(propertyValue is TProperty)) throw new InvalidCastException(string.Format("Imposible convertir al tipo solicitado la propiedad: {0}", propertyName));

            return (TProperty)propertyValue;
        }

        #endregion Methods
    }
}

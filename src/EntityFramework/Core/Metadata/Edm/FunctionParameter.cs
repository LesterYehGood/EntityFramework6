// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Core.Metadata.Edm
{
    using System.Data.Entity.Core.Common;
    using System.Data.Entity.Utilities;

    /// <summary>
    ///     Class representing a function parameter
    /// </summary>
    public sealed class FunctionParameter : MetadataItem, INamedDataModelItem
    {
        internal static Func<FunctionParameter, SafeLink<EdmFunction>> DeclaringFunctionLinker = fp => fp._declaringFunction;

        private readonly SafeLink<EdmFunction> _declaringFunction = new SafeLink<EdmFunction>();

        private readonly TypeUsage _typeUsage;

        private string _name;

        internal FunctionParameter()
        {
            // testing
        }

        /// <summary>
        ///     The constructor for FunctionParameter taking in a name and a TypeUsage object
        /// </summary>
        /// <param name="name"> The name of this FunctionParameter </param>
        /// <param name="typeUsage"> The TypeUsage describing the type of this FunctionParameter </param>
        /// <param name="parameterMode"> Mode of the parameter </param>
        /// <exception cref="System.ArgumentNullException">Thrown if name or typeUsage arguments are null</exception>
        /// <exception cref="System.ArgumentException">Thrown if name argument is empty string</exception>
        internal FunctionParameter(string name, TypeUsage typeUsage, ParameterMode parameterMode)
        {
            Check.NotEmpty(name, "name");
            Check.NotNull(typeUsage, "typeUsage");

            _name = name;
            _typeUsage = typeUsage;

            SetParameterMode(parameterMode);
        }

        /// <summary>
        ///     Gets the built-in type kind for this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.Entity.Core.Metadata.Edm.BuiltInTypeKind" /> object that represents the built-in type kind for this
        ///     <see
        ///         cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />
        ///     .
        /// </returns>
        public override BuiltInTypeKind BuiltInTypeKind
        {
            get { return BuiltInTypeKind.FunctionParameter; }
        }

        /// <summary>
        ///     Gets the mode of this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="T:System.Data.Entity.Core.Metadata.Edm.ParameterMode" /> values.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the FunctionParameter instance is in ReadOnly state</exception>
        [MetadataProperty(BuiltInTypeKind.ParameterMode, false)]
        public ParameterMode Mode
        {
            get { return GetParameterMode(); }
        }

        /// <summary>
        ///     Returns the identity of the member
        /// </summary>
        internal override string Identity
        {
            get { return _name; }
        }
        
        /// <summary>
        ///     Gets the name of this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </summary>
        /// <returns>
        ///     The name of this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </returns>
        [MetadataProperty(PrimitiveTypeKind.String, false)]
        public String Name
        {
            get { return _name; }
            set
            {
                Check.NotEmpty(value, "value");

                _name = value;
            }
        }

        /// <summary>
        ///     Gets the instance of the <see cref="T:System.Data.Entity.Core.Metadata.Edm.TypeUsage" /> class that contains both the type of the parameter and facets for the type.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.Entity.Core.Metadata.Edm.TypeUsage" /> object that contains both the type of the parameter and facets for the type.
        /// </returns>
        [MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
        public TypeUsage TypeUsage
        {
            get { return _typeUsage; }
        }

        public string TypeName
        {
            get { return TypeUsage.EdmType.Name; }
        }

        public bool IsMaxLengthConstant
        {
            get
            {
                Facet facet;
                return
                    TypeUsage.Facets.TryGetValue(DbProviderManifest.MaxLengthFacetName, false, out facet)
                    && facet.Description.IsConstant;
            }
        }

        public int? MaxLength
        {
            get
            {
                Facet facet;
                return TypeUsage.Facets.TryGetValue(DbProviderManifest.MaxLengthFacetName, false, out facet)
                           ? facet.Value as int?
                           : null;
            }
        }

        public bool IsMaxLength
        {
            get
            {
                Facet facet;
                return TypeUsage.Facets.TryGetValue(DbProviderManifest.MaxLengthFacetName, false, out facet)
                       && facet.IsUnbounded;
            }
        }

        public bool IsPrecisionConstant
        {
            get
            {
                Facet facet;
                return
                    TypeUsage.Facets.TryGetValue(DbProviderManifest.PrecisionFacetName, false, out facet)
                    && facet.Description.IsConstant;
            }
        }

        public byte? Precision
        {
            get
            {
                Facet facet;
                return TypeUsage.Facets.TryGetValue(DbProviderManifest.PrecisionFacetName, false, out facet)
                           ? facet.Value as byte?
                           : null;
            }
        }

        public bool IsScaleConstant
        {
            get
            {
                Facet facet;
                return
                    TypeUsage.Facets.TryGetValue(DbProviderManifest.ScaleFacetName, false, out facet)
                    && facet.Description.IsConstant;
            }
        }

        public byte? Scale
        {
            get
            {
                Facet facet;
                return TypeUsage.Facets.TryGetValue(DbProviderManifest.ScaleFacetName, false, out facet)
                           ? facet.Value as byte?
                           : null;
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:System.Data.Entity.Core.Metadata.Edm.EdmFunction" /> on which this parameter is declared.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.Entity.Core.Metadata.Edm.EdmFunction" /> object that represents the function on which this parameter is declared.
        /// </returns>
        public EdmFunction DeclaringFunction
        {
            get { return _declaringFunction.Value; }
        }

        /// <summary>
        ///     Returns the name of this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </summary>
        /// <returns>
        ///     The name of this <see cref="T:System.Data.Entity.Core.Metadata.Edm.FunctionParameter" />.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Sets the member to read only mode. Once this is done, there are no changes
        ///     that can be done to this class
        /// </summary>
        internal override void SetReadOnly()
        {
            if (!IsReadOnly)
            {
                base.SetReadOnly();
                // TypeUsage is always readonly, no reason to set it
            }
        }

        /// <summary>
        ///     The factory method for constructing the <see cref="FunctionParameter" /> object.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="edmType">The EdmType of the parameter.</param>
        /// <param name="parameterMode">
        ///     The <see cref="ParameterMode" /> of the parameter.
        /// </param>
        /// <returns>
        ///     A new, read-only instance of the <see cref="EdmFunction" /> type.
        /// </returns>
        public static FunctionParameter Create(string name, EdmType edmType, ParameterMode parameterMode)
        {
            Check.NotNull(name, "name");
            Check.NotNull(edmType, "edmType");

            var functionParameter =
                new FunctionParameter(name, TypeUsage.Create(edmType, FacetValues.NullFacetValues), parameterMode);

            functionParameter.SetReadOnly();

            return functionParameter;
        }
    }
}

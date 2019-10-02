using Elements.Geometry;
using Elements.Geometry.Solids;
//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.0.24.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace Elements
{
    #pragma warning disable // Disable all warnings

    /// <summary>A representation component comprised of one or more geometries.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.24.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Representation3D : Component
    {
        /// <summary>A collection of geometries.</summary>
        [Newtonsoft.Json.JsonProperty("Geometries", Required = Newtonsoft.Json.Required.Always)]
        public System.Collections.Generic.List<Solid> Geometries { get; set; } = new System.Collections.Generic.List<Solid>();
    
    
    }
}
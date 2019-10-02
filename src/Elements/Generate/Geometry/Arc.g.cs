using Elements.Geometry;
using Elements.Geometry.Solids;
//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.0.24.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace Elements.Geometry
{
    #pragma warning disable // Disable all warnings

    /// <summary>A linear curve between two points.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.24.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Arc : Curve
    {
        /// <summary>The center of the arc.</summary>
        [Newtonsoft.Json.JsonProperty("Center", Required = Newtonsoft.Json.Required.AllowNull)]
        public Vector3 Center { get; set; }
    
        /// <summary>The angle from 0.0, in degrees, at which the arc will start with respect to the positive X axis.</summary>
        [Newtonsoft.Json.JsonProperty("StartAngle", Required = Newtonsoft.Json.Required.Always)]
        public double StartAngle { get; set; }
    
        /// <summary>The angle from 0.0, in degrees, at which the arc will end with respect to the positive X axis.</summary>
        [Newtonsoft.Json.JsonProperty("EndAngle", Required = Newtonsoft.Json.Required.Always)]
        public double EndAngle { get; set; }
    
        /// <summary>The radius of the arc.</summary>
        [Newtonsoft.Json.JsonProperty("Radius", Required = Newtonsoft.Json.Required.Always)]
        public double Radius { get; set; }
    
    
    }
}
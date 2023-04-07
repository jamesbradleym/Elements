using Elements;
using Elements.Geometry;
using Elements.Tests;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Hypar.Tests
{
    public class ArcTests : ModelTest
    {
        ITestOutputHelper _output;

        public ArcTests(ITestOutputHelper output)
        {
            this._output = output;
            this.GenerateIfc = false;
        }

        [Fact, Trait("Category", "Examples")]
        public void Arc()
        {
            this.Name = "Elements_Geometry_Arc";

            // <example>
            var arc = new Arc(Vector3.Origin, 2.0, 0.0, 90.0);
            Assert.True(new Vector3(2, 0, 0).IsAlmostEqualTo(arc.Start));
            Assert.True(new Vector3(0, 2, 0).IsAlmostEqualTo(arc.End));

            var arc1 = new Arc(Vector3.Origin, 2.0, 0.0, -90.0);
            Assert.True(new Vector3(2, 0, 0).IsAlmostEqualTo(arc1.Start));
            Assert.True(new Vector3(0, -2, 0).IsAlmostEqualTo(arc1.End));

            // A transformed arc.
            var arc2 = new Arc(new Transform(Vector3.Origin, Vector3.XAxis), 2.0, 0, Math.PI);
            // </example>

            this.Model.AddElement(new ModelCurve(arc, BuiltInMaterials.XAxis));
            this.Model.AddElement(new ModelCurve(arc1, BuiltInMaterials.YAxis));
            this.Model.AddElement(new ModelCurve(arc2, BuiltInMaterials.ZAxis));
        }

        [Fact]
        public void GetTransformsTransformedCurveSucceeds()
        {
            this.Name = nameof(GetTransformsTransformedCurveSucceeds);
            var arc = new Arc(new Transform(Vector3.Origin, Vector3.XAxis), 5, 0, Math.PI);
            var parameters = arc.GetSampleParameters();
            foreach (var p in parameters)
            {
                var t = arc.TransformAt(p);
                this.Model.AddElements(t.ToModelCurves());
            }
            this.Model.AddElement(new ModelCurve(arc, BuiltInMaterials.ZAxis));
        }

        [Fact]
        public void GetSampleParametersReversedCurveSucceeds()
        {
            var arc = new Arc(Vector3.Origin, 2.0, 0.0, -90.0);
            var parameters = arc.GetSampleParameters();
            foreach (var p in parameters)
            {
                arc.PointAt(p);
            }
        }

        [Fact]
        public void ZeroSweep_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Arc(Vector3.Origin, 2.0, 0.0, 0.0));
        }

        [Fact]
        public void ZeroRadius_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Arc(Vector3.Origin, 0.0, 0.0, 90.0));
        }

        [Fact]
        public void PointAt()
        {
            var arc = new Arc(Vector3.Origin, 5.0, 0.0, 90.0);
            Assert.Equal(new Vector3(0, 5, 0), arc.End);
            Assert.Equal(new Vector3(0, 5, 0), arc.PointAt(arc.Domain.Max));
            Assert.Equal(new Vector3(5 * Math.Cos(Math.PI / 4), 5 * Math.Sin(Math.PI / 4), 0), arc.PointAt(arc.Domain.Length / 2));
            Assert.Equal(new Vector3(5 * Math.Cos(Math.PI / 2), 5 * Math.Sin(Math.PI / 2), 0), arc.PointAt(arc.Domain.Max));

            arc = new Arc(Vector3.Origin, 5.0, 0.0, 180.0);
            Assert.Equal(new Vector3(-5, 0, 0), arc.PointAt(arc.Domain.Max));
            Assert.Equal(new Vector3(0, 5, 0), arc.PointAt(arc.Domain.Length / 2));
            Assert.Equal(new Vector3(5, 0, 0), arc.PointAt(arc.Domain.Min));
            Assert.Equal(new Vector3(5, 0, 0), arc.PointAt(arc.Domain.Min + -1e-15));
        }

        [Fact]
        public void TransformAt()
        {
            var arc = new Arc(Vector3.Origin, 5.0, 0.0, 180.0);
            var t = arc.TransformAt(Math.PI / 2);
            Assert.Equal(new Vector3(0, 1, 0), t.XAxis);
            Assert.Equal(new Vector3(0, 0, 1), t.YAxis);
            Assert.Equal(new Vector3(1, 0, 0), t.ZAxis);
        }

        [Fact]
        public void Frames()
        {
            var arc = new Arc(Vector3.Origin, 5.0, 0.0, 180.0);
            var frames = arc.Frames();

            var arc1 = new Arc(Vector3.Origin, 5.0, 0.0, 180.0);
            var frames1 = arc.Frames(0.1, 0.1);
        }

        [Fact]
        public void Complement()
        {
            var arc = new Arc(Vector3.Origin, 1, 10, 20);
            var comp = arc.Complement();
            Assert.Equal(-340, comp.StartAngle);
            Assert.Equal(10, comp.EndAngle);

            arc = new Arc(Vector3.Origin, 1, -10, 10);
            comp = arc.Complement();
            Assert.Equal(-350, comp.StartAngle);
            Assert.Equal(-10, comp.EndAngle);
        }

        [Fact]
        public void ToPolyline()
        {
            var arc = new Arc(Vector3.Origin, 1, 10, 20);
            var p = arc.ToPolyline(10);
            Assert.Equal(10, p.Segments().Length);
            Assert.Equal(arc.Start, p.Vertices[0]);
            Assert.Equal(arc.End, p.Vertices[p.Vertices.Count - 1]);
        }

        [Fact]
        public void ToPolygon()
        {
            var c = new Circle(Vector3.Origin, 1);
            var p = c.ToPolygon(10);
            Assert.Equal(10, p.Segments().Length);
            Assert.Equal(c.PointAt(0), p.Vertices[0]);
            Assert.Equal(c.PointAt(Math.PI * 2), p.Vertices[0]);
        }
    }
}
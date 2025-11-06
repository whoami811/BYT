using System;
using NUnit.Framework;

namespace TestsForUML
{
    public class Tests
    {
        private readonly IShape sphere = new Sphere(5);

        [Test]
        public void TestSphereCalculateArea()
        {
            Assert.That(sphere.CalculateArea(), Is.EqualTo(314.159).Within(0.001));
        }

        [Test]
        public void TestSphereCalculateVolume()
        {
            Assert.That(sphere.CalculateVolume(), Is.EqualTo(523.598).Within(0.001));
        }
        
        private readonly IShape clylinder = new Cylinder(5, 5);
        
        [Test]
        public void TestCylinderCalculateArea()
        {
            Assert.That(clylinder.CalculateArea(), Is.EqualTo(314.159).Within(0.001));
        }

        [Test]
        public void TestCylinderCalculateVolume()
        {
            Assert.That(clylinder.CalculateVolume(), Is.EqualTo(392.699).Within(0.001));
        }

        private readonly IShape rectangle = new Rectangle(5, 10);
        
        [Test]
        public void TestRectangleCalculateArea()
        {
            Assert.That(rectangle.CalculateArea(), Is.EqualTo(50).Within(0.001));
        }

        [Test]
        public void TestRectangleCalculateVolume()
        {
            Assert.That(rectangle.CalculateVolume(), Is.EqualTo(0).Within(0.001));
        }
        
        private readonly IShape cube = new Cube(5);
        
        [Test]
        public void TestCubeCalculateArea()
        {
            Assert.That(cube.CalculateArea(), Is.EqualTo(150).Within(0.001));
        }

        [Test]
        public void TestCubeCalculateVolume()
        {
            Assert.That(cube.CalculateVolume(), Is.EqualTo(125).Within(0.001));
        }
    }
}
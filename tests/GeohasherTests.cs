using GeoTools;
using System;
using Xunit;

namespace Geohash.Test
{
    public class GeohashBaseTests
    {
        [Fact]
        public void Should_Encode_WithDefaultPrecison()
        {
            var hasher = new Geohasher();

            var hash = hasher.Encode(52.5174, 13.409);

            Assert.Equal("u33dc0", hash);
        }

        [Fact]
        public void Should_Encode_WithGivenPrecision()
        {
            var hasher = new Geohasher();

            var hash = hasher.Encode(52.517395, 13.408813, 12);

            Assert.Equal("u33dc07zzzzx", hash);
        }


        [Fact]
        public void Should_Decode_Precision6()
        {
            var hasher = new Geohasher();

            var hash = hasher.Decode("u33dc0");

            Assert.Equal(52.5174, Math.Round(hash.Item1, 4));
            Assert.Equal(13.409, Math.Round(hash.Item2, 3));
        }

        [Fact]
        public void Should_Decode_Precision12()
        {
            var hasher = new Geohasher();

            var hash = hasher.Decode("u33dc07zzzzx");

            Assert.Equal(52.51739494, Math.Round(hash.Item1,8)); 
            Assert.Equal(13.40881297, Math.Round(hash.Item2,8));
        }

        [Fact]
        public void Should_Give_Subhashes()
        {
            var hasher = new Geohasher();

            var subhashes = hasher.GetSubhashes("u33dc0");

            Assert.Equal(32, subhashes.Length);
        }

        [Fact]
        public void Should_Give_Neighbors()
        {
            var hasher = new Geohasher();

            var subhashes = hasher.GetNeighbors("u33dc0");

            Assert.Equal("u33dc1", subhashes[Direction.North]);
            Assert.Equal("u33dc3", subhashes[Direction.NorthEast]);
            Assert.Equal("u33dc2", subhashes[Direction.East]);
            Assert.Equal("u33d9r", subhashes[Direction.SouthEast]);
            Assert.Equal("u33d9p", subhashes[Direction.South]);
            Assert.Equal("u33d8z", subhashes[Direction.SouthWest]);
            Assert.Equal("u33dbb", subhashes[Direction.West]);
            Assert.Equal("u33dbc", subhashes[Direction.NorthWest]);
        }

        [Fact]
        public void Should_Give_Neighbor()
        {
            var hasher = new Geohasher();

            Assert.Equal("u33dc1", hasher.GetNeighbor("u33dc0", Direction.North));
            Assert.Equal("u33dc3", hasher.GetNeighbor("u33dc0", Direction.NorthEast));
            Assert.Equal("u33dc2", hasher.GetNeighbor("u33dc0", Direction.East));
            Assert.Equal("u33d9r", hasher.GetNeighbor("u33dc0", Direction.SouthEast));
            Assert.Equal("u33d9p", hasher.GetNeighbor("u33dc0", Direction.South));
            Assert.Equal("u33d8z", hasher.GetNeighbor("u33dc0", Direction.SouthWest));
            Assert.Equal("u33dbb", hasher.GetNeighbor("u33dc0", Direction.West));
            Assert.Equal("u33dbc", hasher.GetNeighbor("u33dc0", Direction.NorthWest));
        }

        [Fact]
        public void Should_Give_Parent()
        {
            var hasher = new Geohasher();
            Assert.Equal("u33db", hasher.GetParent("u33dbc"));
        }

        [Fact]
        public void Should_Throw_Given_Incorrect_Lat()
        {
            var hasher = new Geohasher();

            Assert.Throws<ArgumentException>(()=> hasher.Encode(152.517395, 13.408813, 12));
        }

        [Fact]
        public void Should_Throw_Given_Incorrect_Lng()
        {
            var hasher = new Geohasher();

            Assert.Throws<ArgumentException>(() => hasher.Encode(52.517395, 183.408813, 12));
        }
    }
}
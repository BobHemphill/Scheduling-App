using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer.Validation;
using DomainLayer;

namespace DomainLayerTests.Validation {
    [TestFixture]
    public class BlockCollectionValidatorTest {
        [Test]
        public void ValidateFullYearCoverage() {
            var validator = new BlockCollectionValidator();

            Assert.IsFalse(validator.Validate(new List<Block>()));
            Assert.IsFalse(validator.Validate(new List<Block> { new Block { StartDate = new DateTime(2011, 1, 2), EndDate = new DateTime(2011, 12, 31) } }));
            Assert.IsFalse(validator.Validate(new List<Block> { new Block { StartDate = new DateTime(2011, 2, 1), EndDate = new DateTime(2011, 12, 31) } }));
            Assert.IsFalse(validator.Validate(new List<Block> { new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 12, 30) } }));
            Assert.IsFalse(validator.Validate(new List<Block> { new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 10, 31) } }));

            Assert.IsTrue(validator.Validate(new List<Block> { new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 12, 31) } }));
        }

        [Test]
        public void ValidateFullYearCoverage_MultipleBlocks() {
            var validator = new BlockCollectionValidator();

            Assert.IsTrue(validator.Validate(new List<Block> { 
                new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 6, 30) },
                new Block { StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 12, 31) }}));
        }

        [Test]
        public void ValidateFullYearCoverage_FullYear_Valid() {
             var validator = new BlockCollectionValidator();

             Assert.IsTrue(validator.Validate(new List<Block> { 
                new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 1, 31) },
                new Block { StartDate = new DateTime(2011, 2, 1), EndDate = new DateTime(2011, 2, 28) },
                new Block { StartDate = new DateTime(2011, 3, 1), EndDate = new DateTime(2011, 3, 31) },
                new Block { StartDate = new DateTime(2011, 4, 1), EndDate = new DateTime(2011, 4, 30) },
                new Block { StartDate = new DateTime(2011, 5, 1), EndDate = new DateTime(2011, 5, 31) },
                new Block { StartDate = new DateTime(2011, 6, 1), EndDate = new DateTime(2011, 6, 30) },
                new Block { StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 7, 31) },
                new Block { StartDate = new DateTime(2011, 8, 1), EndDate = new DateTime(2011, 8, 31) },
                new Block { StartDate = new DateTime(2011, 9, 1), EndDate = new DateTime(2011, 9, 30) },
                new Block { StartDate = new DateTime(2011, 10, 1), EndDate = new DateTime(2011, 10, 31) },
                new Block { StartDate = new DateTime(2011, 11, 1), EndDate = new DateTime(2011, 11, 30) },
                new Block { StartDate = new DateTime(2011, 12, 1), EndDate = new DateTime(2011, 12, 31) }}));
        }

        [Test]
        public void ValidateFullYearCoverage_FullYear_Invalid() {
             var validator = new BlockCollectionValidator();

             Assert.IsFalse(validator.Validate(new List<Block> { 
                new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 1, 30) },
                new Block { StartDate = new DateTime(2011, 2, 1), EndDate = new DateTime(2011, 12, 31) }}));

             Assert.IsFalse(validator.Validate(new List<Block> { 
                new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 1, 31) },
                new Block { StartDate = new DateTime(2011, 2, 2), EndDate = new DateTime(2011, 12, 31) }}));
        }
    }
}

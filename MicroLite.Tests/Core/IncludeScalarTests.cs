﻿namespace MicroLite.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using MicroLite.Core;
    using MicroLite.Tests.TestEntities;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="IncludeScalar&lt;T&gt;"/> class.
    /// </summary>
    public class IncludeScalarTests
    {
        /// <summary>
        /// Issue #172 - Cannot use Session.Include.Scalar to return an enum
        /// </summary>
        public class ForAnEnumTypeWhenBuildValueHasBeenCalledAndThereAreNoResults
        {
            private IncludeScalar<CustomerStatus> include = new IncludeScalar<CustomerStatus>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAnEnumTypeWhenBuildValueHasBeenCalledAndThereAreNoResults()
            {
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeDefaultValue()
            {
                Assert.Equal(default(CustomerStatus), this.include.Value);
            }
        }

        /// <summary>
        /// Issue #172 - Cannot use Session.Include.Scalar to return an enum
        /// </summary>
        public class ForAnEnumTypeWhenBuildValueHasBeenCalledAndThereIsOneResult
        {
            private IncludeScalar<CustomerStatus> include = new IncludeScalar<CustomerStatus>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAnEnumTypeWhenBuildValueHasBeenCalledAndThereIsOneResult()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x.GetInt32(0)).Returns(1);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal(CustomerStatus.Active, this.include.Value);
            }
        }

#if !NET35 && !NET40

        public class ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereAreNoResults
        {
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereAreNoResults()
            {
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { false }).Dequeue);

                this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait();
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeNull()
            {
                Assert.Null(this.include.Value);
            }
        }

        public class ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereIsACallbackRegistered
        {
            private bool callbackCalled = false;
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereIsACallbackRegistered()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns("Foo");
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, this.include));
                this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait();
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheCallbackShouldBeCalled()
            {
                Assert.True(this.callbackCalled);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal("Foo", this.include.Value);
            }
        }

        public class ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereIsOneResult
        {
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueAsyncHasBeenCalledAndThereIsOneResult()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns("Foo");
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait();
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal("Foo", this.include.Value);
            }
        }

#endif

        public class ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereAreNoResults
        {
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereAreNoResults()
            {
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeNull()
            {
                Assert.Null(this.include.Value);
            }
        }

        public class ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereIsACallbackRegistered
        {
            private bool callbackCalled = false;
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereIsACallbackRegistered()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns("Foo");
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, this.include));
                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheCallbackShouldBeCalled()
            {
                Assert.True(this.callbackCalled);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal("Foo", this.include.Value);
            }
        }

        public class ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereIsOneResult
        {
            private IncludeScalar<string> include = new IncludeScalar<string>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAReferenceTypeWhenBuildValueHasBeenCalledAndThereIsOneResult()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns("Foo");
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal("Foo", this.include.Value);
            }
        }

        public class ForAReferenceTypeWhenBuildValueHasNotBeenCalled
        {
            private IncludeScalar<string> include = new IncludeScalar<string>();

            public ForAReferenceTypeWhenBuildValueHasNotBeenCalled()
            {
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeNull()
            {
                Assert.Null(this.include.Value);
            }
        }

#if !NET35 && !NET40

        public class ForAValueTypeWhenBuildValueAsyncHasBeenCalledAndThereAreNoResults
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAValueTypeWhenBuildValueAsyncHasBeenCalledAndThereAreNoResults()
            {
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { false }).Dequeue);

                this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait();
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeDefaultValue()
            {
                Assert.Equal(0, this.include.Value);
            }
        }

        public class ForAValueTypeWhenBuildValueAsyncHasBeenCalledAndThereIsOneResult
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAValueTypeWhenBuildValueAsyncHasBeenCalledAndThereIsOneResult()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns(10);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait();
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal(10, this.include.Value);
            }
        }

#endif

        public class ForAValueTypeWhenBuildValueHasBeenCalledAndThereAreNoResults
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAValueTypeWhenBuildValueHasBeenCalledAndThereAreNoResults()
            {
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeDefaultValue()
            {
                Assert.Equal(0, this.include.Value);
            }
        }

        public class ForAValueTypeWhenBuildValueHasBeenCalledAndThereIsOneResult
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public ForAValueTypeWhenBuildValueHasBeenCalledAndThereIsOneResult()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns(10);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, false }).Dequeue);

                this.include.BuildValue(this.mockReader.Object);
            }

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void TheDataReaderShouldBeRead()
            {
                this.mockReader.VerifyAll();
            }

            [Fact]
            public void ValueShouldBeSetToTheResult()
            {
                Assert.Equal(10, this.include.Value);
            }
        }

        public class ForAValueTypeWhenBuildValueHasNotBeenCalled
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();

            public ForAValueTypeWhenBuildValueHasNotBeenCalled()
            {
            }

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeDefaultValue()
            {
                Assert.Equal(0, this.include.Value);
            }
        }

        public class WhenCallingBuildValueAndTheDataReaderContainsMultipleColumns
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public WhenCallingBuildValueAndTheDataReaderContainsMultipleColumns()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(2);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, true }).Dequeue);
            }

            [Fact]
            public void BuildValueShouldThrowAMicroLiteException()
            {
                var exception = Assert.Throws<MicroLiteException>(() => include.BuildValue(mockReader.Object));

                Assert.Equal(ExceptionMessages.IncludeScalar_MultipleColumns, exception.Message);
            }
        }

        public class WhenCallingBuildValueAndTheDataReaderContainsMultipleResults
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public WhenCallingBuildValueAndTheDataReaderContainsMultipleResults()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns(10);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, true }).Dequeue);
            }

            [Fact]
            public void BuildValueShouldThrowAMicroLiteException()
            {
                var exception = Assert.Throws<MicroLiteException>(() => include.BuildValue(mockReader.Object));

                Assert.Equal(ExceptionMessages.Include_SingleRecordExpected, exception.Message);
            }
        }

#if !NET35 && !NET40

        public class WhenCallingBuildValueAsyncAndTheDataReaderContainsMultipleColumns
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public WhenCallingBuildValueAsyncAndTheDataReaderContainsMultipleColumns()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(2);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, true }).Dequeue);
            }

            [Fact]
            public void BuildValueAsyncShouldThrowAMicroLiteException()
            {
                var exception = Assert.Throws<AggregateException>(
                    () => this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait());

                Assert.IsType<MicroLiteException>(exception.InnerException);
                Assert.Equal(ExceptionMessages.IncludeScalar_MultipleColumns, exception.InnerException.Message);
            }
        }

        public class WhenCallingBuildValueAsyncAndTheDataReaderContainsMultipleResults
        {
            private IncludeScalar<int> include = new IncludeScalar<int>();
            private Mock<IDataReader> mockReader = new Mock<IDataReader>();

            public WhenCallingBuildValueAsyncAndTheDataReaderContainsMultipleResults()
            {
                this.mockReader.Setup(x => x.FieldCount).Returns(1);
                this.mockReader.Setup(x => x[0]).Returns(10);
                this.mockReader.Setup(x => x.Read()).Returns(new Queue<bool>(new[] { true, true }).Dequeue);
            }

            [Fact]
            public void BuildValueAsyncShouldThrowAMicroLiteException()
            {
                var exception = Assert.Throws<AggregateException>(
                    () => this.include.BuildValueAsync(new MockDbDataReaderWrapper(this.mockReader.Object), CancellationToken.None).Wait());

                Assert.IsType<MicroLiteException>(exception.InnerException);
                Assert.Equal(ExceptionMessages.Include_SingleRecordExpected, exception.InnerException.Message);
            }
        }

#endif
    }
}
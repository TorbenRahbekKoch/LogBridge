using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FluentAssertions;
using SoftwarePassion.Common.Extensions;
using Xunit;

namespace SoftwarePassion.LogBridge.Describers.Tests.Unit
{
    /// <summary>
    /// MethodDescriptor Tests.
    /// Note that the [MethodImpl(MethodImplOptions.NoOptimization)] is necessary to avoid inlining
    /// of the called methods.
    /// </summary>
    public class When_Describing_Methods
    {
        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Simple_Parameters_Is_Correct()
        {
            var description1 = Methods.Method1(42, 43, 44, 45, true, false);
            var description2 = Methods.Method2("Value");
            var description3 = Methods.Method3(42.87);
            var description4 = Methods.Method4(42, "Value", 42.87);

            description1.Should().Be(Namespace + "Method1(value1: 42, value2: 43, value3: 44, value4: 45, value5: True, value6: False)", "Description 1 incorrect.");
            description2.Should().Be(Namespace + "Method2(value: \"Value\")", "Description 2 incorrect.");
            description3.Should().Be(Namespace + "Method3(value: 42.87)", "Description 3 incorrect.");
            description4.Should().Be(Namespace + "Method4(value1: 42, value2: \"Value\", value3: 42.87)", "Description 4 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_ICollection_Parameters_Is_Correct()
        {
            var description5 = Methods.Method5(new List<int>() { 1, 2 });

            var expected = Namespace + "Method5(value: [{0}  1,{0}  2])".FormatInvariant(Environment.NewLine);
            description5.Should().Be(expected, "Description 5 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_IDictionary_Parameters_Is_Correct()
        {
            var values = new Dictionary<string, int>()
            {
                {"42", 43},
                {"87", 88}
            };

            var description6 = Methods.Method6(values);

            description6.Should().Be(Namespace + "Method6(value: [{0}  [\"42\":43],{0}  [\"87\":88]])".FormatInvariant(Environment.NewLine), "Description 6 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Enum_Parameters_Is_Correct()
        {
            var description7 = Methods.Method7(Enums.Enum1);

            description7.Should().Be(Namespace + "Method7(value: Enums.Enum1)", "Description 7 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Guid_Parameters_Is_Correct()
        {
            var guid = Guid.NewGuid();
            var description8 = Methods.Method8(guid);

            description8.Should().Be(Namespace + "Method8(value: {0})".FormatInvariant(guid), "Description 8 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Class_Parameters_Is_Correct()
        {
            var class1 = new Class1 {Property1 = "1", Property2 = "2"};
            var description9 = Methods.Method9(class1);

            string expected = Namespace + "Method9(value: {0}[{1}  Property1: \"1\",{1}  Property2: \"2\"])".FormatInvariant(ClassNameSpace + "Class1", Environment.NewLine);
            description9.Should().Be(expected,  "Description 9 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Complex_Class_Parameters_Is_Correct()
        {
            var class2 = new Class2 {PropertyA = new Class1() {Property1 = "A", Property2 = "B"}, PropertyB = 42.87m};
            var description10 = Methods.Method10(class2);

            string expected = Namespace + "Method10(value: {0}[{2}  PropertyA: {1}[{2}    Property1: \"A\",{2}    Property2: \"B\"],{2}  PropertyB: 42.87])"
                                .FormatInvariant(ClassNameSpace + "Class2", ClassNameSpace + "Class1", Environment.NewLine);
            description10.Should().Be(expected, "Description 10 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_DateTime_Parameters_Is_Correct()
        {
            var dateTimea = new DateTime(2012, 10, 25, 12, 34, 56, DateTimeKind.Utc);
            var dateTimeb = new DateTime(2012, 10, 25, 12, 34, 56, DateTimeKind.Local);
            var description11a = Methods.Method11(dateTimea);
            var description11b = Methods.Method11(dateTimeb);

            description11a.Should().Be(Namespace + "Method11(value: DateTimeKind.Utc:2012-10-25 12:34:56.000)", "Description 11a incorrect.");
            description11b.Should().Be(Namespace + "Method11(value: DateTimeKind.Local:2012-10-25 12:34:56.000)", "Description 11b incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Null_Parameters_Is_Correct()
        {
            var description12 = Methods.Method12(null);

            description12.Should().Be(Namespace + "Method12(value: null)", "Description 12 is incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Class_Parameters_With_Null_Strings_Is_Correct()
        {
            var class1 = new Class1 { Property1 = "1", Property2 = null };
            var description13 = Methods.Method9(class1);

            var expected = Namespace + "Method9(value: {0}[{1}  Property1: \"1\",{1}  Property2: null])".FormatInvariant(ClassNameSpace + "Class1", Environment.NewLine);
            description13.Should().Be(expected, "Description 13 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_Class_Parameters_With_Null_Properties_Is_Correct()
        {
            var class2 = new Class2 { PropertyA = null, PropertyB = 42.27m };
            var description14 = Methods.Method10(class2);

            var expected = Namespace + "Method10(value: {0}[{1}  PropertyA: null,{1}  PropertyB: 42.27])".FormatInvariant(ClassNameSpace + "Class2", Environment.NewLine);
            description14.Should().Be(expected, "Description 14 incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]        
        public void Verify_That_Description_Of_Nullable_DateTime_Is_Correct()
        {
            DateTime? dateTimea = null;
            DateTime? dateTimeb = new DateTime(2012, 10, 25, 12, 34, 56, DateTimeKind.Local);
            var description15a = Methods.Method15(dateTimea);
            var description15b = Methods.Method15(dateTimeb);
             
            description15a.Should().Be(Namespace + "Method15(value: System.DateTime: null)", "Description 15a incorrect.");
            description15b.Should().Be(Namespace + "Method15(value: DateTimeKind.Local:2012-10-25 12:34:56.000)", "Description 15b incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_object_Property_Is_Correct()
        {
            var description16a = Methods.Method16("Text");
            var description16b = Methods.Method16(42);

            description16a.Should().Be(Namespace + "Method16(value: \"{0}\")".FormatInvariant("Text"), "Description 16a incorrect.");
            description16b.Should().Be(Namespace + "Method16(value: {0})".FormatInvariant(42), "Description 16b incorrect.");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_IEnumerable_Parameters_Is_Correct()
        {
            var description17 = Methods.Method17(new Class3());
            description17.Should().Be(Namespace + "Method17(value: [{0}  27,{0}  42])".FormatInvariant(Environment.NewLine));
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_Of_TimeSpan_Is_Correct()
        {
            var timespan = new TimeSpan(42, 17, 13, 57, 123);

            var description18 = Methods.Method18(timespan);
            description18.ParameterDescription.Should().Be(Namespace + "Method18(value: 42.17:13:57.1230000)");
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Verify_That_Description_From_Lambda_Is_Correct()
        {
            var value = 42;
            var descriptionLambda = Methods.LambdaMethod(value);
            descriptionLambda.ParameterDescription.Should().Be(Namespace + "LambdaMethod(value: 42)");
        }

        private const string ClassNameSpace = "SoftwarePassion.LogBridge.Describers.Tests.Unit.";
        private const string Namespace = "SoftwarePassion.LogBridge.Describers.Tests.Unit.Methods.";
    }
}
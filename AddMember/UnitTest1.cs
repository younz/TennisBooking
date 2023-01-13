using System.Collections.Generic;
using NUnit.Framework;
using TennisBooking.Models;

namespace AddMember
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Member member = new Member();
            List<Member> members = new List<Member>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
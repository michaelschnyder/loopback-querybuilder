using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoopbackQueryBuilder.Tests
{
    [TestClass]
    public class ConcreteModelTests
    {
        private class Job
        {
            public int UserId { get; set; }

            public int MediaCardId { get; set; }

            public bool IsMasterDvd { get; set; }

            public DateTime? CreatedDateTime { get; set; }

            public DateTime? AssignedDateTime { get; set; }

            public DateTime? FinishedDateTime { get; set; }

            public string Status { get; set; }

            public string JobFileName { get; set; }

            public int Id { get; set; }

            public int? DiskMakerId { get; set; }
        }

        [TestMethod]
        public void QueryByMediaCardAndMasterDvdMade()
        {
            var builder = new LoopbackQueryBuilder<Job>();

            var query = builder.Where(j => j.MediaCardId == 12 && j.IsMasterDvd == true);

            Assert.IsNotNull(query);
            Debug.WriteLine(query);
        }
    }
}

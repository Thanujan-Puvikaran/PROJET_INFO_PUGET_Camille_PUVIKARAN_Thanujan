using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan;

namespace TestUnitaire
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            byte[] tab = new byte[4] { 1, 0, 0, 0 };
            //Assert.AreEqual(a.Taille(tab), 1);

            Fractale_image_cache a = new Fractale_image_cache();
            
            //int a = 8;
            //int b = 0;
            Assert.AreEqual(a.From_byte_To_int(tab),8);

        }
        [TestMethod]
        public void TestMethod2()
        {
            byte[] tab = new byte[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
            byte[] tab2 = new byte[8] { 1, 1, 1, 1, 0, 0, 0, 0 };
            Fractale_image_cache p = new Fractale_image_cache();
            byte value=p.retrieving(tab, tab2);
            Assert.AreEqual(value, 143);
        }
        [TestMethod]
        public void TestMethod3()
        {
            byte[] tab = new byte[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
            byte[] tab2 = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            Fractale_image_cache p = new Fractale_image_cache();
            byte value = p.returning(tab,1);
            Assert.AreEqual(value, 128);
        }
    }
}

using System;
using CulinaryRecipes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CulinaryRecipesTests
{
    [TestClass]
    public class ZmianaDzialaniaRtxtAmountsTest
    {
        [TestMethod]
        public void CzyPodczasTworzeniaBuforJestRoznyOdNull()
        {
            var buf = new RichTextBoxMy(4);
            var buf1 = new RichTextBoxMy();

            Assert.IsTrue(!buf.ListaJestPusta);
            Assert.IsTrue(!buf1.ListaJestPusta);
        }

        [TestMethod]
        public void PobierzLinie()
        {
           

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaChess;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class GameTests
    {


        [TestMethod]
        public void TestValidateColorInput()
        {

            Assert.AreEqual(true, Game.ValidateColorInput(""));
            Assert.AreEqual(true, Game.ValidateColorInput("w"));
            Assert.AreEqual(true, Game.ValidateColorInput("W"));
            Assert.AreEqual(true, Game.ValidateColorInput("white"));
            Assert.AreEqual(true, Game.ValidateColorInput("WHITE"));
            Assert.AreEqual(true, Game.ValidateColorInput("wHiTe"));
            Assert.AreEqual(true, Game.ValidateColorInput("WhItE"));
            Assert.AreEqual(true, Game.ValidateColorInput("b"));
            Assert.AreEqual(true, Game.ValidateColorInput("B"));
            Assert.AreEqual(true, Game.ValidateColorInput("black"));
            Assert.AreEqual(true, Game.ValidateColorInput("BLACK"));
            Assert.AreEqual(true, Game.ValidateColorInput("bLaCk"));
            Assert.AreEqual(true, Game.ValidateColorInput("BlAcK"));

            Assert.AreEqual(false, Game.ValidateColorInput("a"));
            Assert.AreEqual(false, Game.ValidateColorInput("A"));
            Assert.AreEqual(false, Game.ValidateColorInput("c"));
            Assert.AreEqual(false, Game.ValidateColorInput("c"));
            Assert.AreEqual(false, Game.ValidateColorInput("aAbD"));


        }


        [TestMethod]
        public void TestParseColorInput()
        {

            Assert.AreEqual(true, Game.ParseColorInput("w"));
            Assert.AreEqual(true, Game.ParseColorInput("W"));
            Assert.AreEqual(true, Game.ParseColorInput("white"));
            Assert.AreEqual(true, Game.ParseColorInput("WHITE"));
            Assert.AreEqual(true, Game.ParseColorInput("wHiTe"));
            Assert.AreEqual(true, Game.ParseColorInput("WhItE"));


            Assert.AreEqual(false, Game.ParseColorInput("b"));
            Assert.AreEqual(false, Game.ParseColorInput("B"));
            Assert.AreEqual(false, Game.ParseColorInput("black"));
            Assert.AreEqual(false, Game.ParseColorInput("BLACK"));
            Assert.AreEqual(false, Game.ParseColorInput("bLaCk"));
            Assert.AreEqual(false, Game.ParseColorInput("BlAcK"));


        }


    }
}

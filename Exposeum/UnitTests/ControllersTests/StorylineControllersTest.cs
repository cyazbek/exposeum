using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Exposeum;
using Exposeum.Controllers;
using Exposeum.Models;

using NUnit.Framework;

namespace UnitTests.ControllersTests
{
    [TestFixture]
    class StorylineControllerTest
    {
        private MapController m_inst;
        private Map map;
        private StorylineController s_inst;
        private List<StoryLine> list;

        [SetUp]
        public void Setup()
        {
            map = Map.GetInstance();
            list = map.GetStoryLineList;
            //m_inst = MapController.GetInstance(Android.App.Application.Context);
            s_inst = StorylineController.GetInstance();
        }

        [TearDown]
        public void Tear()
        {
        }

        [Test]
        public void getInstanceTest()
        {
            Assert.IsNotNull(StorylineController.GetInstance());
        }

        [Test]
        public void GetStoryLinesTest()
        {
            map = Map.GetInstance();
            Assert.AreEqual(StorylineController.GetInstance().GetStoryLines().Count, map.GetStoryLineList.Count);
        }

        [Test]
        public void ResetStorylineTest()
        {
            StoryLine toReset = list[0];
            toReset.MapElements[0].SetVisited();
            toReset.MapElements[1].SetVisited();

            s_inst.ResetStorylineProgress(toReset);
            Assert.True(!toReset.MapElements[0].Visited);
        }
    }
}
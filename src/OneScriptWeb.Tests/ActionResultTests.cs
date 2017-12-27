﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OneScript.WebHost.Application;
using OneScript.WebHost.Infrastructure;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using Xunit;

namespace OneScriptWeb.Tests
{
    public class ActionResultTests
    {
        // инициализирует систему типов
        private static WebApplicationEngine wa = new WebApplicationEngine();

        [Fact]
        public void ViewResultDataAccessibleThroughScript()
        {
            var osResult = new ViewActionResult();
            osResult.ViewData = InitViewData();
            osResult.ViewData["MyData"] = ValueFactory.Create("string data");

            Assert.Equal("string data", osResult.ViewData.GetIndexedValue(ValueFactory.Create("MyData")).AsString());
        }

        private ViewDataDictionaryWrapper InitViewData()
        {
            var dict = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            return new ViewDataDictionaryWrapper(dict);
        }

        [Fact]
        public void ViewResultDataChangedInRealObject()
        {
            var osResult = new ViewActionResult();
            osResult.ViewData = InitViewData();
            osResult.ViewData["MyData"] = ValueFactory.Create("string data");

            var objectFromRealDict = ((ViewDataDictionary) osResult.ViewData.UnderlyingObject)["MyData"];
            Assert.Equal("string data", ((IValue)objectFromRealDict).AsString());
        }

        [Fact]
        public void ViewResultModelIsSet()
        {
            var osResult = new ViewActionResult();
            osResult.ViewData = InitViewData();
            osResult.ViewData.Model = "HELLO";
            var result = (ViewResult) osResult.UnderlyingObject;
            Assert.Equal("HELLO", (string)result.Model);
        }
    }
}
﻿using EPiCore.ViewModels.Pages;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class ResultQueryModel : SourceInfoModel
    {

        public bool Use { get; set; }
        public bool UseBefore { get; set; }
    }
}
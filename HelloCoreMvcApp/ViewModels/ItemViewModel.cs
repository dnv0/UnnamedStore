﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
    }
}
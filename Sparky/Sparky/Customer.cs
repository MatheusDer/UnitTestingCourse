﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky;

public class Customer
{
    public string GreetMessage { get; set; }

    public string CombineNames(string firstName, string lastName)
        => $"{firstName} {lastName}";

    public string GreetAndCombineNames(string firstName, string lastName)
        => GreetMessage = $"Hello, {firstName} {lastName}";
}

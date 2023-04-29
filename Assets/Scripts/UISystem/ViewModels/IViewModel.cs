using Isekai.Components;
using Isekai.UI.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Isekai.UI.Models.Model;

namespace Isekai.UI.ViewModels
{
    public interface IViewModel
    {
        event PropertyValueChangedEventHandler PropertyValueChanged;
    }
}


﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Edit
{
    public class EditField<T> : ComponentBase
    {
        [Parameter]
        public string AuthOR { get; set; }

        [Parameter]
        public string AuthAND { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Display { get; set; }

        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        public Task OnValueChanged(ChangeEventArgs e)
        {
            this.Value = (T)e.Value;
            return this.ValueChanged.InvokeAsync(this.Value);
        }

    }
}
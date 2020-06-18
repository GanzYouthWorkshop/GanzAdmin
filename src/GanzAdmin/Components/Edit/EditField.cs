using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Edit
{
    public class EditField<T> : ComponentBase
    {
        private bool m_HasRendered = false;

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
        public T Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }
        private T m_Value;

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<T> Changed { get; set; }

        protected virtual Task OnValueChanged(ChangeEventArgs e)
        {
            this.m_Value = (T)e.Value;

            this.Changed.InvokeAsync(this.m_Value);
            return this.ValueChanged.InvokeAsync(this.m_Value);
            
        }

        protected override void OnAfterRender(bool firstRender)
        {
            this.m_HasRendered = true;
        }

        protected override bool ShouldRender()
        {
            return !this.m_HasRendered;
        }
    }
}

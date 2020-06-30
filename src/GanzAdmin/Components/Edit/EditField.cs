using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.Components.Edit
{
    public class EditField<T> : ComponentBase
    {
        protected bool m_HasRendered = false;

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
        public bool PreventRenderOnChange { get; set; } = true;

        [Parameter]
        public T Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }
        protected T m_Value;

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<T> Changed { get; set; }

        //Namost ez itt ronda. De lemondom miért!
        //Ha egy érték változik az egész szülő-komponsens újrarajzolódik és ez a legtöbb esetben baromság. De azért néha szükség lehet rá.
        //Mivel más lehetőség nincs a jelzésre, ezért a Stack trace-ben megnézünk egy értéket és ha bennevan, akkor nem nemm frissítani (pl. OnValueChanged)
        //De ha nha szükség van rá ezért más értéknek kell a stack trace-ben lennie mint amúgy ezért kétféle metódus ugyanazt csinálja de - ugye milyen cseles? - más a nevük
        //és így meg lehet őket különböztetni :D.
        #region ValueChanged kezelés
        protected virtual Task OnValueChanged(ChangeEventArgs e)
        {
            this.m_Value = (T)e.Value;

            this.Changed.InvokeAsync(this.m_Value);

            if(this.PreventRenderOnChange)
            {
                return this.OnValueChangedOnly();
            }
            else
            {
                return this.OnValueChangedWithRender();
            }
        }

        protected Task OnValueChangedOnly()
        {
            return this.ValueChanged.InvokeAsync(this.m_Value);
        }

        protected Task OnValueChangedWithRender()
        {
            return this.ValueChanged.InvokeAsync(this.m_Value);
        }
        #endregion


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

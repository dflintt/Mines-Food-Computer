﻿#pragma checksum "C:\Users\Duncan\Documents\Visual Studio 2015\Projects\FoodComputerC\FoodComputerCHeaded\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9E62FA329E1FE518753D9340FA8696E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodComputerCHeaded
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.testMoistureBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 2:
                {
                    this.ActuateIndicator = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 3:
                {
                    this.PollIndicator = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 4:
                {
                    this.SetIndicator = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 5:
                {
                    this.Actuate = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 15 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.Actuate).SelectionChanged += this.textBlock_SelectionChanged;
                    #line default
                }
                break;
            case 6:
                {
                    this.textBlock_Copy = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.textBlock_Copy1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.button = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


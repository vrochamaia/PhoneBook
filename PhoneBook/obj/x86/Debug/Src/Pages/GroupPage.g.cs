﻿#pragma checksum "C:\Users\vroch\Documents\Visual Studio 2015\Projects\PhoneBook\PhoneBook\Src\Pages\GroupPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2DF22C499D4B95F337847BBF17B12332"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneBook.Src.Pages
{
    partial class GroupPage : 
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
                    this.griHeader = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.txtNoGroupsFounded = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.progressBarListViewGroups = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 4:
                {
                    this.listViewGroups = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 68 "..\..\..\..\Src\Pages\GroupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.listViewGroups).Tapped += this.listViewGroups_Tapped;
                    #line default
                }
                break;
            case 5:
                {
                    this.btnCreateGroup = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 19 "..\..\..\..\Src\Pages\GroupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnCreateGroup).Click += this.onClickBtnCreateGroup;
                    #line default
                }
                break;
            case 6:
                {
                    this.txtSearchedValue = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 28 "..\..\..\..\Src\Pages\GroupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.txtSearchedValue).KeyUp += this.onKeyUpTxtSearchedValue;
                    #line default
                }
                break;
            case 7:
                {
                    this.btnSearch = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 29 "..\..\..\..\Src\Pages\GroupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnSearch).Click += this.onClickBtnSearch;
                    #line default
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


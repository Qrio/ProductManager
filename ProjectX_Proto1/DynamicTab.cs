﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectX_Proto2
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ProjectX_Proto2"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ProjectX_Proto2;assembly=ProjectX_Proto2"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DynamicTab/>
    ///
    /// </summary>
    public class DynamicTab : TabItem
    {
        static DynamicTab()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicTab), new FrameworkPropertyMetadata(typeof(DynamicTab)));
        }

        public static readonly RoutedEvent DynamicTabEvent = EventManager.RegisterRoutedEvent("CloseTab", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DynamicTab));

        public event RoutedEventHandler CloseTab
        {
            add { AddHandler(DynamicTabEvent, value); }
            remove { RemoveHandler(DynamicTabEvent, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button closeButton = base.GetTemplateChild("PART_Close") as Button;
            if(closeButton != null)
                closeButton.Click += new System.Windows.RoutedEventHandler(closeButton_Click);
        }
        void closeButton_Click(object sender,System.Windows.RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(DynamicTabEvent, this));
        }
    }
}

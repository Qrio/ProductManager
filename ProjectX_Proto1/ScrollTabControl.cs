using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Collections.Specialized;

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
    ///     <MyNamespace:ScrollTabControl/>
    ///
    /// </summary>
    public class ScrollTabControl : TabControl
    {
        private const int ScrollStep = 25;

        private RepeatButton tabLeftButton;
        private RepeatButton tabRightButton;
        //private Button tabAddItemButton;
        private ScrollViewer tabScrollViewer;
        private Panel tabPanelTop;

        static ScrollTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollTabControl), new FrameworkPropertyMetadata(typeof(ScrollTabControl)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.tabLeftButton = GetTemplateChild("TabLeftButtonTop") as RepeatButton;
            this.tabRightButton = GetTemplateChild("TabRightButtonTop") as RepeatButton;
            this.tabScrollViewer = GetTemplateChild("TabScrollViewerTop") as ScrollViewer;
            this.tabPanelTop = GetTemplateChild("HeaderPanel") as Panel;

            if (this.tabLeftButton != null)
                this.tabLeftButton.Click += tabLeftButton_Click;

            if (this.tabRightButton != null)
                this.tabRightButton.Click += tabRightButton_Click;

            this.tabScrollViewer.Loaded += (s, e) => this.UpdateScrollButtonsAvailability();
            this.tabScrollViewer.ScrollChanged += (s, e) => this.UpdateScrollButtonsAvailability();

            this.SelectionChanged += (s, e) => this.ScrollToSelectedItem();
        }

        /// <summary>
        /// Gets or sets the Tab Top Left Button style.
        /// </summary>
        /// <value>The left button style style.</value>
        [Description("Gets or sets the tab top left button style")]
        [Category("ScrollButton")]
        public Style TabLeftButtonTopStyle
        {
            get { return (Style)GetValue(TabLeftButtonTopStyleProperty); }
            set { SetValue(TabLeftButtonTopStyleProperty, value); }
        }
        /// <summary>
        /// Gets or sets the Tab Top Right Button style.
        /// </summary>
        /// <value>The left button style style.</value>
        [Description("Gets or sets the tab top right button style")]
        [Category("ScrollButton")]
        public Style TabRightButtonTopStyle
        {
            get { return (Style)GetValue(TabRightButtonTopStyleProperty); }
            set { SetValue(TabRightButtonTopStyleProperty, value); }
        }

        /// <summary>
        /// Tab Top Left Button style
        /// </summary>
        public static readonly DependencyProperty TabLeftButtonTopStyleProperty = DependencyProperty.Register(
            "TabLeftButtonTopStyle",
            typeof(Style),
            typeof(ScrollTabControl),
            new PropertyMetadata(null));

        /// <summary>
        /// Tab Top Left Button style
        /// </summary>
        public static readonly DependencyProperty TabRightButtonTopStyleProperty = DependencyProperty.Register(
            "TabRightButtonTopStyle",
            typeof(Style),
            typeof(ScrollTabControl),
            new PropertyMetadata(null));

        private void tabRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.tabScrollViewer && null != this.tabPanelTop)
            {
                // added margin left for sure that the item will be scrolled
                var rightItemOffset = Math.Min(this.tabScrollViewer.HorizontalOffset + this.tabScrollViewer.ViewportWidth + this.tabPanelTop.Margin.Left, this.tabScrollViewer.ExtentWidth);

                var rightItem = this.GetItemByOffset(rightItemOffset);
                this.ScrollToItem(rightItem);
            }
        }
        private void tabLeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.tabScrollViewer)
            {
                var leftItemOffset = Math.Max(this.tabScrollViewer.HorizontalOffset - this.tabPanelTop.Margin.Left, 0);

                var leftItem = this.GetItemByOffset(leftItemOffset);
                this.ScrollToItem(leftItem);
            }

        }

        /// <summary>
        /// Change visibility and avalability of buttons if it is necessary
        /// </summary>
        /// <param name="horizontalOffset">the real offset instead of outdated one from the scroll viewer</param>
        private void UpdateScrollButtonsAvailability(double? horizontalOffset = null)
        {
            if (this.tabScrollViewer == null) return;

            var hOffset = horizontalOffset ?? this.tabScrollViewer.HorizontalOffset;
            hOffset = Math.Max(hOffset, 0);

            var scrWidth = this.tabScrollViewer.ScrollableWidth;
            scrWidth = Math.Max(scrWidth, 0);

            if (this.tabLeftButton != null)
            {
                this.tabLeftButton.Visibility = scrWidth == 0 ? Visibility.Collapsed : Visibility.Visible;

                this.tabLeftButton.IsEnabled = hOffset > 0;
            }
            if (this.tabRightButton != null)
            {
                this.tabRightButton.Visibility = scrWidth == 0 ? Visibility.Collapsed : Visibility.Visible;

                this.tabRightButton.IsEnabled = hOffset < scrWidth;
            }
        }

        /// <summary>
        /// Scrolls to a selected tab
        /// </summary>
        private void ScrollToSelectedItem()
        {
            var model = base.SelectedItem;
            var si = this.ItemContainerGenerator.ContainerFromItem(model) as DynamicTab;
            if (si == null || this.tabScrollViewer == null)
                return;
            if (si.ActualWidth == 0 && !si.IsLoaded)
            {
                si.Loaded += (s, e) => ScrollToSelectedItem();
                return;
            }

            this.ScrollToItem(si);
        }

        /// <summary>
        /// Scrolls to the specified tab
        /// </summary>
        private void ScrollToItem(DynamicTab si)
        {
            var tabItems = this.Items.Cast<object>()
                .Select(item => this.ItemContainerGenerator.ContainerFromItem(item) as DynamicTab);

            var leftItems = tabItems
                .Where(ti => ti != null)
                .TakeWhile(ti => ti != si).ToList();

            var leftItemsWidth = leftItems.Sum(ti => ti.ActualWidth);

            //If the selected item is situated somewhere at the right area
            if (leftItemsWidth + si.ActualWidth > this.tabScrollViewer.HorizontalOffset + this.tabScrollViewer.ViewportWidth)
            {
                var currentHorizontalOffset = (leftItemsWidth + si.ActualWidth) - this.tabScrollViewer.ViewportWidth;
                // the selected item has extra width, so I add it to the offset
                var hMargin = !leftItems.Any(ti => ti.IsSelected) && !si.IsSelected ? this.tabPanelTop.Margin.Left + this.tabPanelTop.Margin.Right : 0;
                currentHorizontalOffset += hMargin;

                this.tabScrollViewer.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
            //if the selected item somewhere at the left
            else if (leftItemsWidth < this.tabScrollViewer.HorizontalOffset)
            {
                var currentHorizontalOffset = leftItemsWidth;
                // the selected item has extra width, so I remove it from the offset
                var hMargin = leftItems.Any(ti => ti.IsSelected) ? this.tabPanelTop.Margin.Left + this.tabPanelTop.Margin.Right : 0;
                currentHorizontalOffset -= hMargin;

                this.tabScrollViewer.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
        }

        /// <summary>
        /// Returns the tab item by using some kind of a hit-test
        /// </summary>
        /// <param name="offset">the absolute coordinate in pixels starting from the left</param>
        private DynamicTab GetItemByOffset(double offset)
        {
            var tabItems = this.Items.Cast<object>()
                .Select(item => this.ItemContainerGenerator.ContainerFromItem(item) as DynamicTab)
                .ToList();

            double currentItemsWidth = 0;
            // get tabs one by one and calculate their aggregated width until the offset value is reached
            foreach (var ti in tabItems)
            {
                if (currentItemsWidth + ti.ActualWidth >= offset)
                    return ti;

                currentItemsWidth += ti.ActualWidth;
            }

            return tabItems.LastOrDefault();
        }
    }
}

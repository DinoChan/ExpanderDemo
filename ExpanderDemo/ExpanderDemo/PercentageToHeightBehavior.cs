using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ExpanderDemo
{
    public class PercentageToHeightBehavior : Behavior<StackPanel>
    {
        /// <summary>
        /// 获取或设置ContentElement的值
        /// </summary>  
        public FrameworkElement ContentElement
        {
            get { return (FrameworkElement)GetValue(ContentElementProperty); }
            set { SetValue(ContentElementProperty, value); }
        }

        /// <summary>
        /// 标识 ContentElement 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ContentElementProperty =
            DependencyProperty.Register("ContentElement", typeof(FrameworkElement), typeof(PercentageToHeightBehavior), new PropertyMetadata(null, OnContentElementChanged));

        private static void OnContentElementChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PercentageToHeightBehavior target = obj as PercentageToHeightBehavior;
            FrameworkElement oldValue = (FrameworkElement)args.OldValue;
            FrameworkElement newValue = (FrameworkElement)args.NewValue;
            if (oldValue != newValue)
                target.OnContentElementChanged(oldValue, newValue);
        }

        protected virtual void OnContentElementChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
            if (oldValue != null)
                newValue.SizeChanged -= OnContentElementSizeChanged;

            if (newValue != null)
                newValue.SizeChanged += OnContentElementSizeChanged;
        }

        private void OnContentElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTargetHeight();
        }


        /// <summary>
        /// 获取或设置Percentage的值
        /// </summary>  
        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        /// <summary>
        /// 标识 Percentage 依赖属性。
        /// </summary>
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(PercentageToHeightBehavior), new PropertyMetadata(1d, OnPercentageChanged));

        private static void OnPercentageChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PercentageToHeightBehavior target = obj as PercentageToHeightBehavior;
            double oldValue = (double)args.OldValue;
            double newValue = (double)args.NewValue;
            if (oldValue != newValue)
                target.OnPercentageChanged(oldValue, newValue);
        }

        protected virtual void OnPercentageChanged(double oldValue, double newValue)
        {
            UpdateTargetHeight();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateTargetHeight()
        {
            double height = 0;
            if (ContentElement == null || ContentElement.ActualHeight == 0 || double.IsNaN(Percentage))
                height = double.NaN;
            else
                height = ContentElement.ActualHeight * Percentage;

            if (AssociatedObject != null)
                AssociatedObject.Height = height;
        }
    }
}

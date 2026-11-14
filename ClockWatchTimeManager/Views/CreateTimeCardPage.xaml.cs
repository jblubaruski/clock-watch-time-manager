using ClockWatchTimeManager.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace ClockWatchTimeManager.Views;

public partial class CreateTimeCardPage : ContentPage
{
    // styles
    private Style? HorizontalSeparatorStyle { get; set; } = null;
    private Style? LabelStyle { get; set; } = null;
    private Style? DescriptionBorderStyle { get; set; } = null;
    private Style? DescriptionLabelStyle { get; set; } = null;
    private Style? ButtonStyle { get; set; } = null;

    public CreateTimeCardPage()
    {
        InitializeComponent();
        LoadStyles();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((CreateTimeCardViewModel)BindingContext).AddBreakEntryBoxRequested += AddBreakEntryBox;
        ((CreateTimeCardViewModel)BindingContext).AddBreakDisplayBoxRequested += AddBreakDisplayBox;
        ((CreateTimeCardViewModel)BindingContext).ClearBreakDisplayRequested += ClearBreakDisplay;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((CreateTimeCardViewModel)BindingContext).AddBreakEntryBoxRequested -= AddBreakEntryBox;
        ((CreateTimeCardViewModel)BindingContext).AddBreakDisplayBoxRequested -= AddBreakDisplayBox;
        ((CreateTimeCardViewModel)BindingContext).ClearBreakDisplayRequested -= ClearBreakDisplay;
    }

    // loads each style used in this code-behind file and logs an error in the log if the style could not be found
    private void LoadStyles()
    {
        if (Application.Current != null)
        {
            string key = "horizontalSeparatorStyle";
            if (Application.Current.Resources.TryGetValue(key, out object value) && value is Style)
                HorizontalSeparatorStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "labelStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                LabelStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "descriptionBorderStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                DescriptionBorderStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "descriptionLabelStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                DescriptionLabelStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "buttonStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                ButtonStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));
        }
    }

    private static void LogLoadResourceError(string errorLocation, string resourceKey, string resourceType)
    {
        App.logger.LogEntry
            (
                $"In {errorLocation}:\n",
                $"{resourceKey} does not exist as a {resourceType} in the resource dictionary"
            );
    }

    // adds a display box to breakDisplay
    private void AddBreakDisplayBox(string breakStartTime, string breakEndTime)
    {
        // break entry container

        Grid entryContainer = new()
        {
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(80)),
                new ColumnDefinition(new GridLength(100)),
                new ColumnDefinition(new GridLength(280))
            }
        };

        // top border

        BoxView topBorder = new()
        {
            Style = HorizontalSeparatorStyle
        };
        entryContainer.AddWithSpan(topBorder, 0, 0, 1, 3);

        // start time

        Label startTimeLabel = new()
        {
            Text = "Start Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeLabel, 1, 0);

        Label startTimeValue = new()
        {
            Text = breakStartTime,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeValue, 1, 1, 1, 2);

        // end time

        Label endTimeLabel = new()
        {
            Text = "End Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeLabel, 2, 0);

        Label endTimeValue = new()
        {
            Text = breakEndTime,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeValue, 2, 1, 1, 2);

        // break description

        Border descriptionBorder = new()
        {
            Style = DescriptionBorderStyle,
            WidthRequest = 415,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new(4)
            }
        };
        entryContainer.AddWithSpan(descriptionBorder, 3, 0, 1, 3);

        Label descriptionValue = new()
        {
            Text = CreateTimeCardViewModel.DEFAULT_BREAK_DESCRIPTION,
            Style = DescriptionLabelStyle,
            VerticalTextAlignment = TextAlignment.Center,
            WidthRequest = 410
        };
        descriptionBorder.Content = descriptionValue;
    }

    // adds an entry box to breakDisplay
    private void AddBreakEntryBox()
    {
        // break entry container

        Grid entryContainer = new()
        {
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(80)),
                new ColumnDefinition(new GridLength(100)),
                new ColumnDefinition(new GridLength(280))
            }
        };

        // top border

        BoxView topBorder = new()
        {
            Style = HorizontalSeparatorStyle
        };
        entryContainer.AddWithSpan(topBorder, 0, 0, 1, 3);

        // start time

        Label startTimeLabel = new()
        {
            Text = "Start Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeLabel, 1, 0);

        Label startTimeValue = new()
        {
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        startTimeValue.SetBinding(Label.TextProperty, "CurrentBreakStartTime");
        entryContainer.AddWithSpan(startTimeValue, 1, 1, 1, 2);

        // end time

        Label endTimeLabel = new()
        {
            Text = "End Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeLabel, 2, 0);

        Label endTimeValue = new()
        {
            Text = CreateTimeCardViewModel.DEFAULT_DATETIME,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        endTimeValue.SetBinding(Label.TextProperty, "CurrentBreakEndTime");
        entryContainer.AddWithSpan(endTimeValue, 2, 1, 1, 2);

        // break description

        Border descriptionBorder = new()
        {
            Style = DescriptionBorderStyle,
            WidthRequest = 415,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new(4)
            }
        };
        entryContainer.AddWithSpan(descriptionBorder, 3, 0, 1, 3);

        Label descriptionValue = new()
        {
            Text = CreateTimeCardViewModel.DEFAULT_BREAK_DESCRIPTION,
            Style = DescriptionLabelStyle,
            VerticalTextAlignment = TextAlignment.Center,
            WidthRequest = 410
        };
        descriptionBorder.Content = descriptionValue;

        // edit description button

        Button editDescriptionButton = new()
        {
            Text = "Edit Break Description",
            Style = ButtonStyle,
            WidthRequest = 275,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new(0,5,0,5)
        };
        entryContainer.AddWithSpan(editDescriptionButton, 4, 0, 1, 3);

        // add container to breakDisplay
        breakDisplay.Children.Add(entryContainer);
    }

    private void ClearBreakDisplay()
    {
        breakDisplay.Children.Clear();
    }
}
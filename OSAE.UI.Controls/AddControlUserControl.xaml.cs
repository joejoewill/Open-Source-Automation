﻿
namespace OSAE.UI.Controls
{
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic; 

    /// <summary>
    /// Interaction logic for AddControlUserControl.xaml
    /// </summary>
    public partial class AddControlUserControl : UserControl
    {
        private OSAE osae = new OSAE("OSAE.UI.Controls");
        string currentScreen;

        public AddControlUserControl(string screen)
        {
            InitializeComponent();
            currentScreen = screen;
            LoadUserControls();
        }

        private void LoadUserControls()
        {
            DataSet dataSet = osae.RunSQL("SELECT object_type FROM osae_v_object_type WHERE base_type = 'USER CONTROL' order by object_type");
            typesComboBox.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string sName = "Screen - " + currentScreen + " - " + txtName.Text;
            osae.ObjectAdd(sName, sName, "USER CONTROL", "", currentScreen, true);
            osae.ObjectPropertySet(sName, "Control Type", typesComboBox.Text);
            osae.ObjectPropertySet(sName, "X", "100");
            osae.ObjectPropertySet(sName, "Y", "100");
            osae.ObjectPropertySet(sName, "ZOrder", "1");

            osae.ScreenObjectAdd(currentScreen, currentScreen, sName);

            NotifyParentFinished();
        }

        private void cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            NotifyParentFinished();
        }

        /// <summary>
        /// Let the hosting contol know that we are done
        /// </summary>
        /// <remarks>At present it tells the parent to close, this could later be altered to have a event that fires to
        /// the parent allowing them to decide what to do when the control is finished. If the control is being hosted in
        /// an element host this will have no affect as the parent is the element host and not the form.</remarks>
        private void NotifyParentFinished()
        {
            // Get the window hosting us so we can ask it to close
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}

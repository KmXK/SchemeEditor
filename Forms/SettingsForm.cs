using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoScheme.Schemes;

namespace AutoScheme
{
    public partial class SettingsForm : Form
    {
        private SchemeSettings _settings;

        public SchemeSettings Settings => _settings;
        
        public SettingsForm(SchemeSettings settings)
        {
            InitializeComponent();

            _settings = settings;

            blockWidth.Value = _settings.StandartWidth;
            blockHeight.Value = _settings.StandartHeight;
            blocksOnPage.Value = _settings.BlocksOnPage;
            horizontalInterval.Value = _settings.HorizontalInterval;
            verticalInterval.Value = _settings.VerticalInterval;
            pageInterval.Value = _settings.PagesInterval;
            pageOffset.Value = _settings.PageOffset;
            fontSize.Value = _settings.FontSize;
            connectorSize.Value = _settings.ConnectorSize;
            qualityBox.SelectedIndex = (_settings.Quality - 1) / 2;
            firstPage.Value = _settings.FirstPage;
        }

        private void ShowError(string mes)
        {
            MessageBox.Show(mes, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connectorSize.Value > blockWidth.Value)
            {
                ShowError("Ширина соединителя не может превышать ширину блока!");
                return;
            }

            _settings.StandartWidth = (int)blockWidth.Value;
            _settings.StandartHeight = (int)blockHeight.Value;
            _settings.BlocksOnPage = (int) blocksOnPage.Value;
            _settings.HorizontalInterval = (int) horizontalInterval.Value;
            _settings.VerticalInterval = (int) verticalInterval.Value;

            if (_settings.HorizontalInterval % 2 != 0)
                _settings.HorizontalInterval--;
            if (_settings.VerticalInterval % 2 != 0)
                _settings.VerticalInterval--;
            
            _settings.PagesInterval = (int) pageInterval.Value;
            _settings.PageOffset = (int) pageOffset.Value;
            _settings.FontSize = (int) fontSize.Value;
            _settings.ConnectorSize = (int) connectorSize.Value;
            _settings.Quality = qualityBox.SelectedIndex * 2 + 1;
            _settings.FirstPage = (int) firstPage.Value;
        }
    }
}
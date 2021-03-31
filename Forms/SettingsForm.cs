﻿using System;
using System.Windows.Forms;
using SchemeEditor.Schemes;

namespace SchemeEditor
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
            _settings.PagesInterval = (int) pageInterval.Value;
            _settings.PageOffset = (int) pageOffset.Value;
            _settings.FontSize = (int) fontSize.Value;
            _settings.ConnectorSize = (int) connectorSize.Value;
        }
    }
}
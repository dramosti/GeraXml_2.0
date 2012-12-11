using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace NfeGerarXml
{
    public partial class MaskedEditColumn : DataGridViewColumn
    {
        public MaskedEditColumn() : base (new MaskedEditCell())
        {

        }
        public MaskedEditColumn(IContainer container)
        {
            container.Add(this);
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                //Ensure that the cell used for the template is a CalendarCell.
                if (!(value == null) && !(value.GetType().IsAssignableFrom(typeof(MaskedEditCell))))
                {
                    throw new InvalidCastException("Must be a MaskedEditCell");
                }
                base.CellTemplate = value;
            }
        }
        //private string m_strMask;

        //public string Mask
        //{
        //    get
        //    {
        //        return m_strMask;
        //    }
        //    set
        //    {
        //        m_strMask = value;
        //    }
        //}
        public string Mask { get; set; }
        public Type ValidatingType { get; set; }
        private char m_cPromptChar = '_';
        public char PromptChar { get { return m_cPromptChar; } set { m_cPromptChar = value; } }
        private MaskedEditCell MaskedEditCellTemplate
        {
            get
            {
                return this.CellTemplate as MaskedEditCell;
            }
        }
    }

    public class MaskedEditCell : DataGridViewTextBoxCell
    {
        public MaskedEditCell()
        {

        }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            MaskedEditColumn mecol = (MaskedEditColumn)this.OwningColumn;
            MaskedEditingControl ctl = DataGridView.EditingControl as MaskedEditingControl;
            try
            {
                ctl.Text = this.Value.ToString();
            }
            catch (Exception)
            {

                ctl.Text = "";
            }
            ctl.Mask = mecol.Mask;
            ctl.PromptChar = mecol.PromptChar;
            ctl.ValidatingType = mecol.ValidatingType;

        }
        public override Type EditType
        {
            get
            {
                return typeof(MaskedEditingControl);
            }
        }
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }

    public class MaskedEditingControl : MaskedTextBox ,IDataGridViewEditingControl 
    {
        private DataGridView dataGridViewControl;
        private bool valueIsChanged = false;
        private int rowIndexNum;

        public MaskedEditingControl()
        {

        }
        public Object EditingControlFormattedValue
        {
            get { return this.Text; }
            set { this.Text = value.ToString(); }
        }
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            return true;
        }
        public Object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }
        public int EditingControlRowIndex { get { return rowIndexNum; } set { rowIndexNum = value; } }
        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }
        public DataGridView EditingControlDataGridView { get { return dataGridViewControl; } set { dataGridViewControl = value; } }
        public bool EditingControlValueChanged { get { return valueIsChanged; } set { valueIsChanged = value; } }
        public Cursor EditingControlCursor
        {
            get { return base.Cursor; }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            valueIsChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }


        #region IDataGridViewEditingControl Members


        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        #endregion
    }
}


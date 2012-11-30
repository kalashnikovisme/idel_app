using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.BisnessLogic;

namespace idel_app.Forms {
	public partial class RequestCreator : ManagerAppForm {
		public RequestCreator(List<List<string>> req) {
			InitializeComponent();
			this.Show();
		}
	}
}
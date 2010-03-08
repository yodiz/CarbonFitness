using System;

namespace CarbonFitness.BusinessLogic.Exceptions {
	public class IngredientParserException : Exception {
		public IngredientParserException(string message, Exception innerException) : base(message, innerException) {}
		public int ColumnIndex { get; set; }

		public string ColumnContent { get; set; }

		public int RowIndex { get; set; }

		public override string Message
		{
			get
			{
				return base.Message + " Column index: " + ColumnIndex + ", Row index: " + RowIndex + ", Column content: " + ColumnContent + ".";
			}
		}
	}
}
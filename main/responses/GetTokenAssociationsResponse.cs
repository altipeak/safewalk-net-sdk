using System;
using System.Collections.Generic;
using System.Text;

namespace safewalk
{
    public class GetTokenAssociationsResponse : BaseResponse
    {
		#region "vars"
		private List<TokenAssociation> associations;
		#endregion

		#region "constr"

		public GetTokenAssociationsResponse(int httpCode
									, Dictionary<String, String> attributes
									, List<TokenAssociation> associations) : base(httpCode, attributes)
		{
			this.associations = associations;
		}

		public GetTokenAssociationsResponse(int httpCode
			, Dictionary<String, List<String>> errors) : base(httpCode, errors)
		{
			 
		}
		#endregion

		#region "publics"
		public override String ToString()
		{
			var sb = new StringBuilder();
			sb.Append(this.httpCode.ToString()).Append(SEPARATOR);

			if (this.httpCode == 200)
			{
				if (this.associations != null)
				{
					if (this.associations.Count > 0)
					{
						sb.Append("[");
						foreach (var tokenAssociation in this.associations)
						{
							sb.Append("(");
							sb.Append(tokenAssociation);
							sb.Append("), ");
						}
						sb.Append("]");
					}
					else
                    {
						sb.Append("NO ASSOCIATIONS");
					}
				}
				else
                {
					sb.Append("NO ASSOCIATIONS");
				}
			}

			foreach (KeyValuePair<String, List<String>> error in this.errors)
			{
				sb.Append(error.Key).Append(" [");
				foreach (String e in error.Value)
				{
					sb.Append(e).Append(", ");
				}
				sb.Append("]").Append(SEPARATOR);
			}

			return sb.ToString();
		}
		#endregion
	}
}

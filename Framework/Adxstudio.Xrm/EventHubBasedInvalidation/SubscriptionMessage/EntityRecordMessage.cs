/*
  Copyright (c) Microsoft Corporation. All rights reserved.
  Licensed under the MIT License. See License.txt in the project root for license information.
*/

namespace Adxstudio.Xrm.EventHubBasedInvalidation
{
	using System;
	using Microsoft.ServiceBus.Messaging;

	/// <summary>
	/// CrmSubscriptionMessage wrapping a BrokeredMessage indicating a CRM Entity Record change
	/// </summary>
	public class EntityRecordMessage : CrmSubscriptionMessage
	{
		/// <summary>
		/// ObjectId
		/// </summary>
		public Guid ObjectId { get; set; }

		/// <summary>
		/// EntityName
		/// 	Entity logical name for the entity
		/// </summary>
		public string EntityName { get; set; }

		/// <summary>
		/// ObjectType
		/// 	ObjectTypeCode of the entity
		/// </summary>
		public int ObjectType { get; set; }

		/// <summary>
		/// Deserialize the BrokeredMessage message body into a EntityRecordMessage
		/// </summary>
		/// <param name="message">BrokeredMessage message body</param>
		/// <param name="brokeredMessage">BrokeredMessage message</param>
		/// <returns>ICrmSubscriptionMessage</returns>
		internal static ICrmSubscriptionMessage DeserializeMessage(string message, BrokeredMessage brokeredMessage)
		{
			EntityRecordMessage entityRecordMessage = (EntityRecordMessage)CrmSubscriptionMessage.DeserializeMessage(message, typeof(EntityRecordMessage));

			if (entityRecordMessage != null && entityRecordMessage.ValidMessage)
			{
				entityRecordMessage.AppendProperties(brokeredMessage);
				return entityRecordMessage;
			}

			return null;
		}

		/// <summary>
		/// Returns true if the properties are all present, otherwise false
		/// </summary>
		protected override bool ValidMessage
		{
			get
			{
				return this.ObjectId != default(Guid)
						&& this.ObjectType != default(int)
						&& !string.IsNullOrEmpty(this.EntityName)
						&& base.ValidMessage;
			}
		}
	}
}

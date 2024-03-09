/*
  Copyright (c) Microsoft Corporation. All rights reserved.
  Licensed under the MIT License. See License.txt in the project root for license information.
*/

namespace Adxstudio.Xrm.EventHubBasedInvalidation
{
	using System;
	using System.IO;
	using Microsoft.Practices.TransientFaultHandling;

	/// <summary>
	/// Error detection strategy for retry policy for Event Hub based Cache / search-Index invalidation.
	/// </summary>
	public class EventHubInvalidationErrorDetectionStrategy : ITransientErrorDetectionStrategy
	{
		/// <summary>
		/// Flags transient errors.
		/// </summary>
		/// <param name="ex">The error.</param>
		/// <returns>'true' if the error is transient.</returns>
		public bool IsTransient(Exception ex)
		{
			return ex is IOException
					|| ex is UnauthorizedAccessException
					|| ex is TransientNullReferenceException;
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Rs317.Extended
{
	[WireDataContract]
	[GameServerPayload(RsServerNetworkOperationCode.ResetInterfaceButtonState)]
	public sealed class ServerResetInterfaceButtonStatePayload : BaseGameServerPayload
	{
		//Just a command packet.
		
		public ServerResetInterfaceButtonStatePayload()
		{
			
		}
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ydnet
{
	static class PGC
	{
		public const int OK			=  0;
		public const int EFAIL		= -1;
		public const int FALSE		=  0;
		public const int TRUE		=  1;
		public const int OK_AL		=  1;

		public const int PHASE_BEGIN	= 0;
		public const int PHASE_LOBBY	= PHASE_BEGIN + 1;
		public const int PHASE_PLAY		= PHASE_BEGIN + 2;
		public const int PHASE_RESULT	= PHASE_BEGIN + 3;
		public const int PHASE_MAX		= PHASE_BEGIN + 4;
	}
}

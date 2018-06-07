using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Hitachi.Tester.Enums;

namespace Hitachi.Tester.Module
{
    public partial class TesterObject
    {
        #region Fields
        private Queue<BladeEventStruct> bladeEventQueue = new Queue<BladeEventStruct>();
        private ReaderWriterLock bladeEventQueueLock = new ReaderWriterLock();
        private Thread bladeEventsThread;
        private object bladeEventLockObj = new object();

        private delegate void AsyncSendGenericEventDelegate(object sender, StatusEventArgs e, ref StatusEventHandler handler, Queue<BladeEventArgs> queue, string eventName);
        private MemsStateValues lastBunnyStatusSent = MemsStateValues.Unknown;
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Methods
        /// <summary>
        /// Puts items in the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendBladeEventCallback(object sender, BladeEventArgs e)
        {

        }

        /// <summary>
        /// This function pulls items out and sends to remote clients.
        /// Actually it calls the callback function in each client via proxy.
        /// </summary>
        private void doBladeEvents()
        {

        }
        #endregion Methods
    }
}

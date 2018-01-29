//
//	  UnityOSC - Example of usage for OSC receiver
//
//	  Copyright (c) 2012 Jorge Garcia Martin
//	  Last edit: Gerard Llorach 2nd August 2017
//
// 	  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// 	  documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// 	  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
// 	  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// 	  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// 	  of the Software.
//
// 	  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// 	  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// 	  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// 	  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// 	  IN THE SOFTWARE.
//

using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;

namespace Leap.Unity
{
    public class oscControl : MonoBehaviour
    {
        private LeapServiceProvider leapScript;
        List<Hand> hands;

        string palmStr = "/palm";
        string wristStr = "/wrist";

        string[] jointName = { "/knuckle", "/joint1", "/joint2", "/joint3" };

        //private OSCServer myServer;

            //public string outIP = "127.0.0.1";
            //public int outPort = 8765;
            //public int inPort = 5678;
            // Buffer size of the application (stores 100 messages from different servers)
            //public int bufferSize = 100;

        void Start()
        {
            GameObject leapController = GameObject.Find("LeapHandController");
            leapScript = leapController.GetComponent<LeapServiceProvider>();

            // Initialize OSC clients (transmitters)
            OSCHandler.Instance.Init();
        }

        void Update()
        {
            var currentFrame = leapScript.CurrentFrame;
            if (currentFrame.Hands.Count > 0)
            {
                var numLeftHands = 0;
                var numRightHands = 0;

                hands = currentFrame.Hands;

                foreach (var hand in hands)
                {
                    if (hand.IsLeft)
                    {
                        ++numLeftHands;

                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + palmStr + "/x", hand.PalmPosition.x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + palmStr + "/y", hand.PalmPosition.y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + palmStr + "/z", hand.PalmPosition.z);

                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + wristStr + "/x", hand.WristPosition.x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + wristStr + "/y", hand.WristPosition.y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + wristStr + "/z", hand.WristPosition.z);

                        foreach (var finger in hand.Fingers)
                        {
                            for (int joint = 0; joint < 4; joint++)
                            {
                                Vector3 fingerPositions = finger.Bone((Bone.BoneType)joint).NextJoint.ToVector3();

                                String thisJoint = jointName[joint];

                                OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + "/" + finger.Type + thisJoint + "/x", fingerPositions.x);
                                OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + "/" + finger.Type + thisJoint + "/y", fingerPositions.y);
                                OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + numLeftHands + "/" + finger.Type + thisJoint + "/z", fingerPositions.z);
                            }
                        }
                    }
                    
                    if (hand.IsRight)
                    {
                        ++numRightHands;

                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + palmStr + "/x", hand.PalmPosition.x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + palmStr + "/y", hand.PalmPosition.y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + palmStr + "/z", hand.PalmPosition.z);

                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + wristStr + "/x", hand.WristPosition.x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + wristStr + "/y", hand.WristPosition.y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + wristStr + "/z", hand.WristPosition.z);

                        foreach (var finger in hand.Fingers)
                        {
                            for (int joint = 0; joint < 4; joint++)
                            {
                                Vector3 fingerPositions = finger.Bone((Bone.BoneType)joint).NextJoint.ToVector3();

                                String thisJoint = jointName[joint];

                                OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + "/" + finger.Type + thisJoint + "/x", fingerPositions.x);
                                OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + "/" + finger.Type + thisJoint + "/y", fingerPositions.y);
                                OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + numRightHands + "/" + finger.Type + thisJoint + "/z", fingerPositions.z);
                            }
                        }
                    }
                }           // Hands for loop
            }               // if Hands > 0
        }                   // Update()
    }
}                           // Namespace
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

        static string knuckle = "/knuckle";
        static string mcp_joint_1  = "/joint1";
        static string mcp_joint_2 = "/joint2";
        static string mcp_joint_3 = "/joint3";
        static string thumbStr = "/thumb";
        static string indexStr = "/index";
        static string middleStr = "/middle";
        static string ringStr = "/ring";
        static string pinkyStr = "/pinky";
        static string palmStr = "/palm";
        static string wristStr = "/wrist";

        string[] fingerName = {thumbStr, indexStr, middleStr, ringStr, pinkyStr };

        string[] jointName = { knuckle, mcp_joint_1, mcp_joint_2, mcp_joint_3 };

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
                hands = currentFrame.Hands;
                Debug.Log(hands[0]);
            }
      
            foreach (var hand in hands)
            {
                if (hand.IsLeft)
                {
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + palmStr + "/x", hand.PalmPosition.x);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + palmStr + "/y", hand.PalmPosition.y);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + palmStr + "/z", hand.PalmPosition.z);

                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + wristStr + "/x", hand.WristPosition.x);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + wristStr + "/y", hand.WristPosition.y);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Left" + wristStr + "/z", hand.WristPosition.z);

                    foreach (var finger in hand.Fingers)
                    {
                        
                        for (int joint = 0; joint < 4; joint++)
                        {

                            Vector3 fingerPositions = finger.Bone((Bone.BoneType)joint).NextJoint.ToVector3();

                            
                            String thisJoint = jointName[joint];

                            OSCHandler.Instance.SendMessageToClient("myClient", "/Left/" + finger.Type + thisJoint + "/x", fingerPositions.x);
                            OSCHandler.Instance.SendMessageToClient("myClient", "/Left/" + finger.Type + thisJoint + "/y", fingerPositions.y);
                            OSCHandler.Instance.SendMessageToClient("myClient", "/Left/" + finger.Type + thisJoint + "/z", fingerPositions.z);
                        }
                    }
                }

                if (hand.IsRight)
                {
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + palmStr + "/x", hand.PalmPosition.x);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + palmStr + "/y", hand.PalmPosition.y);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + palmStr + "/z", hand.PalmPosition.z);

                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + wristStr + "/x", hand.WristPosition.x);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + wristStr + "/y", hand.WristPosition.y);
                    OSCHandler.Instance.SendMessageToClient("myClient", "/Right" + wristStr + "/z", hand.WristPosition.z);

                    foreach (var finger in hand.Fingers)
                    {

                        for (int joint = 0; joint < 4; joint++)
                        {

                            Vector3 fingerPositions = finger.Bone((Bone.BoneType)joint).NextJoint.ToVector3();


                            String thisJoint = jointName[joint];

                            OSCHandler.Instance.SendMessageToClient("myClient", "/Right/" + finger.Type + thisJoint + "/x", fingerPositions.x);
                            OSCHandler.Instance.SendMessageToClient("myClient", "/Right/" + finger.Type + thisJoint + "/y", fingerPositions.y);
                            OSCHandler.Instance.SendMessageToClient("myClient", "/Right/" + finger.Type + thisJoint + "/z", fingerPositions.z);
                        }
                    }
                }


            }
                /*
            if (leftHand.gameObject.activeSelf == true)
            {
                Chirality handedness = leftHand.GetComponent<CapsuleHand>().Handedness;

                Vector3 palmPosition = leftHand.GetComponent<CapsuleHand>().getPalmPosition();
                Vector3 wristPosition = leftHand.GetComponent<CapsuleHand>().getWristPosition();
                List<Vector3> fingerPositions = leftHand.GetComponent<CapsuleHand>().getFingerPosition();

                for (int i = 0; i <= 19; i += 4 )
                {
                    String thisFinger = fingerName[i / 4];

                    for (int j = 0; j <= 3; j++)
                    {
                        String thisJoint = jointName[j];

                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/x", fingerPositions[i + j].x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/y", fingerPositions[i + j].y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/z", fingerPositions[i + j].z);

                    }
                }

                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/x", palmPosition.x);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/y", palmPosition.y);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/z", palmPosition.z);

                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/x", wristPosition.x);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/y", wristPosition.y);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/z", wristPosition.z);
            }

            if (rightHand.gameObject.activeSelf == true)
            {
                Chirality handedness = rightHand.GetComponent<CapsuleHand>().Handedness;

                Vector3 palmPosition = rightHand.GetComponent<CapsuleHand>().getPalmPosition();
                Vector3 wristPosition = rightHand.GetComponent<CapsuleHand>().getWristPosition();
                List<Vector3> fingerPositions = rightHand.GetComponent<CapsuleHand>().getFingerPosition();

                for (int i = 0; i <= 19; i += 4)
                {
                    String thisFinger = fingerName[i / 4];

                    for (int j = 0; j <= 3; j++)
                    {
                        String thisJoint = jointName[j];

                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/x", fingerPositions[i + j].x);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/y", fingerPositions[i + j].y);
                        OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + thisFinger + thisJoint + "/z", fingerPositions[i + j].z);

                    }
                }

                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/x", palmPosition.x);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/y", palmPosition.y);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + palmStr + "/z", palmPosition.z);

                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/x", wristPosition.x);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/y", wristPosition.y);
                OSCHandler.Instance.SendMessageToClient("myClient", "/" + handedness + wristStr + "/z", wristPosition.z);

            }
            */
        }
        

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Special{
    public static class MyInput{
        public static Vector3 GetMouseWorldPos(){
            Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPosWithZ(){
            return GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPosWithZ(Camera worldCam){
            return GetMouseWorldPosWithZ(Input.mousePosition, worldCam);
        }

        public static Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCam){
            Vector3 worldPos = worldCam.ScreenToWorldPoint(screenPos);
            return worldPos;
        }
    }
}
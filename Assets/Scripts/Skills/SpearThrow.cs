using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills{
    public class SpearThrow : Attack
    {
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override void UseSkill()
        {
            throw new System.NotImplementedException();
        }

        public override float getCoolDown()
        {
            throw new System.NotImplementedException();
        }
    }
}
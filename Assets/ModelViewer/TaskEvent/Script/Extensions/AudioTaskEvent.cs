// author: Stevie Giovanni

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelViewer
{
    // a task event that lets user specifies an initial and goal transform of a part in a multipartsobject
    public class AudioTaskEvent : TaskEvent
    {
        private GameObject _gameObject;
        public GameObject GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }

        private float _volume;
        public float Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        /// <summary>
        /// constructor that deserialize a serializable task event
        /// </summary>
        public AudioTaskEvent(SerializableTaskEvent ste) : base(ste) {
            GameObject = ste.GameObject;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public AudioTaskEvent() : base() { }

        /// <summary>
        /// coroutine that animates the object associated from start to finish
        /// </summary>
        public override IEnumerator TaskEventCoroutine()
        {
            GameObject.GetComponent<AudioSource>().volume = Volume;
            if (Volume <= 0)
                GameObject.GetComponent<AudioSource>().Stop();
            else
                GameObject.GetComponent<AudioSource>().Play();

            yield return null;
        }
    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyBird
{
    public class PipeCollection
    {
        private PipeResources _pipeResources;
        private List<Pipe> _pipes = new List<Pipe>();
        private Random _random = new Random();

        public PipeCollection()
        {
            _pipeResources = new PipeResources();
        }

        public void LoadContent(ContentManager content)
        {
            _pipeResources.LoadContent(content);
        }

        public void Reset()
        {
            _pipes.Clear();
        }

        public void Update(Rectangle window)
        {
            var lastPipe = _pipes.LastOrDefault();
            if (lastPipe == null || lastPipe.Location < window.Right - 200)
            {
                int min = 5;
                int max = Pipe.GetSegmentCount(window) - 4 - Pipe.GapSize;
                int gapStart = _random.Next(min, max);
                _pipes.Add(new Pipe(gapStart, window.Right + 100, _pipeResources));
            }

            var firstPipe = _pipes.FirstOrDefault();
            if (firstPipe != null && firstPipe.Location < window.Left - 100)
            {
                _pipes.Remove(firstPipe);
            }
        }

        public bool CollidesWith(Vector2 position, float radius)
        {
            return _pipes.Any(p => p.CollidesWith(position, radius));
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var pipe in _pipes)
                pipe.Draw(spriteBatch, camera);
        }
    }
}

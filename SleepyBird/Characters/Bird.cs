﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SleepyBird.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SleepyBird
{
    public class Bird
    {
        public const float Radius = 24.0f;

        private Body _body;
        private Sprite _sprite;
        private IControls _controls;

        public Bird(IControls controls)
        {
            _controls = controls;

            _body = new Body()
            {
                InitialPosition = new Vector2(200.0f, 200.0f),
                InitialVelocity = new Vector2(300.0f, 0.0f)
            };
        }

        public void LoadContent(ContentManager contentManager)
        {
            _sprite = new Sprite(contentManager, "Bird0", "Bird1", "Bird2");
            _sprite.Origin = new Vector2(31.0f, 24.0f);
            _sprite.Position = _body.InitialPosition;
        }

        public void Reset(GameTime gameTime)
        {
            _body.InitialTime = (float)gameTime.TotalGameTime.TotalSeconds;
            _body.InitialPosition = new Vector2(200.0f, 200.0f);
            _body.InitialVelocity = new Vector2(300.0f, 0.0f);
            _body.Acceleration = new Vector2(0.0f, 810.0f);
        }

        public void AliveUpdate(GameTime gameTime)
        {
            if (_controls.ReadFlap())
            {
                _body.ChangeVelocity(gameTime, new Vector2(300.0f, -400.0f));
            }

            _sprite.Position = _body.Position(gameTime);
            var velocity = _body.Velocity(gameTime);
            _sprite.Rotation = (float)Math.Atan2(velocity.Y, velocity.X);
            int frame = gameTime.TotalGameTime.Milliseconds / 250;
            _sprite.ImageIndex = frame > 2 ? 1 : frame;
        }

        public void DeadUpdate(GameTime gameTime)
        {
            _sprite.Position = _body.Position(gameTime);
            _sprite.Rotation = _body.Angle(gameTime);
        }

        public void Die(GameTime gameTime)
        {
            var velocity = _body.Velocity(gameTime);
            float angle = (float)Math.Atan2(velocity.Y, velocity.X);
            _body.InitialAngle = angle;
            _body.AngularVelocity = angle;
            _body.ChangeVelocity(gameTime, Vector2.Zero);
        }

        public Vector2 Position
        {
            get { return _sprite.Position; }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            _sprite.Draw(spriteBatch, camera);
        }
    }
}
using LawnMowers.Domain.Interfaces;
using LawnMowers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawnMowers.Logic
{
    public class LawnLogic : ILawnLogic
    {
        private Lawn _lawn;

        public LawnLogic()
        {
            _lawn = new Lawn();
            _lawn.Mowers = new List<Mower>();
        }

        public void BuildLawnAndMowers(string text)
        {
            var textInput = text.Trim().Split('\n');
            textInput = textInput.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            ParseLawnSize(textInput[0]);

            int i = 1;
            while (i < textInput.Count() - 1)
            {
                AddNewMower(new KeyValuePair<string, string>(textInput[i], textInput[i + 1]));
                i += 2;
            }
        }

        public string MoveLawnMowers()
        {
            var output = string.Empty;
            foreach (var mower in _lawn.Mowers)
            {
                var collision = MoveMower(mower);
                if (collision)
                    return "Collision Course! The mowers have collided with either each other or gone outside the bounds of the lawn! Please adjust your inputs and try again.";
                output += $"{mower.FinishingX} {mower.FinishingY} {mower.FinishingCardinal}\n";
            }
            return output;
        }

        private void ParseLawnSize(string lawnSizeString)
        {
            var lawnArray = lawnSizeString.Split(' ');
            int result;

            if (Int32.TryParse(lawnArray[0], out result))
                _lawn.LawnWidth = result;

            if (Int32.TryParse(lawnArray[1], out result))
                _lawn.LawnHeight = result;
        }

        private bool MoveMower(Mower mower)
        {
            foreach (var direction in mower.Instructions)
            {
                switch (direction)
                {
                    case 'L':
                    case 'R':
                        mower.FinishingCardinal = SpinMower(mower.FinishingCardinal, direction);
                        break;
                    case 'M':
                        if (mower.FinishingCardinal == "E" || mower.FinishingCardinal == "W")
                            mower.FinishingX += MoveMowerForward(mower.FinishingCardinal);
                        else if (mower.FinishingCardinal == "N" || mower.FinishingCardinal == "S")
                            mower.FinishingY += MoveMowerForward(mower.FinishingCardinal);
                        break;
                    default:
                        break;
                }
                var collisionCourse = DoesMowerCollideWithAnything(mower);
                if (collisionCourse)
                    return true;
            }
            return false;
        }

        private bool DoesMowerCollideWithAnything(Mower mower)
        {
            if (mower.FinishingX < 0 || mower.FinishingX > _lawn.LawnWidth || mower.FinishingY < 0 || mower.FinishingY > _lawn.LawnHeight)
                return true;

            if (_lawn.Mowers.Where(i => i.FinishingX == mower.FinishingX && i.FinishingY == mower.FinishingY).Count() > 1)
                return true;

            return false;
        }

        private int MoveMowerForward(string finishingCardinal)
        {
            if (finishingCardinal == "N" || finishingCardinal == "E")
                return 1;
            if (finishingCardinal == "S" || finishingCardinal == "W")
                return -1;
            return 0;
        }

        private string SpinMower(string finishingCardinal, char direction)
        {
            switch (finishingCardinal)
            {
                case "N":
                    if (direction == 'R')
                        return "E";
                    else
                        return "W";
                case "E":
                    if (direction == 'R')
                        return "S";
                    else
                        return "N";
                case "S":
                    if (direction == 'R')
                        return "W";
                    else
                        return "E";
                case "W":
                    if (direction == 'R')
                        return "N";
                    else
                        return "S";
            }
            return finishingCardinal;
        }

        private void AddNewMower(KeyValuePair<string, string> mowerInfo)
        {
            var startingArray = mowerInfo.Key.Split(' ');
            var mower = new Mower();
            int result;

            if (Int32.TryParse(startingArray[0], out result))
                mower.StartingX = result;

            if (Int32.TryParse(startingArray[1], out result))
                mower.StartingY = result;

            mower.StartingCardinal = startingArray[2].ToUpper();
            mower.Instructions = mowerInfo.Value.ToUpper().ToCharArray();

            mower.FinishingCardinal = mower.StartingCardinal;
            mower.FinishingX = mower.StartingX;
            mower.FinishingY = mower.StartingY;

            _lawn.Mowers.Add(mower);
        }
    }
}

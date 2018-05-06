using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FunnyRectPresenter : IFunnyRectPresenter
{
	private readonly IFunnyRectView _view;
	private const int FunnyRectRotateActionCode = 0;
	private const int FunnyRectJumpActionCode = 1;
	private const int FunnyRectChangeColorActionCode = 2;
	public FunnyRectPresenter(IFunnyRectView view)
	{         
		_view = view;
	}
	private void OnRectClicked(object sender, EventArgs e)
	{
		_view.DisableInput();
		var action = GenerateRandomAction();
		switch (action)
		{
			case FunnyRectRotateActionCode:
				_view.Rotate();
				break;
			case FunnyRectJumpActionCode:
				_view.Jump();
				break;
			case FunnyRectChangeColorActionCode:
				var color = GenerateRandomColor();
				_view.ChangeColor(color);
				break;
		}
	}
	private void OnRotationEnd(object sender, EventArgs e)
	{
		_view.EnableInput();
	}
	private void OnJumpEnd(object sender, EventArgs e)
	{
		_view.EnableInput();
	}
	private Color GenerateRandomColor()
	{
		var random = new Random();
		var c = Color.white;
		c.r = (float)random.NextDouble();
		c.g = (float)random.NextDouble();
		c.b = (float)random.NextDouble();
		return c;
	}
	private int GenerateRandomAction()
	{
		var random = new Random();
		return random.Next(FunnyRectRotateActionCode, FunnyRectChangeColorActionCode+1);
	}
	public void Initialize()
	{
		_view.Clicked += OnRectClicked;
		_view.RotationEnd += OnRotationEnd;
		_view.JumpEnd += OnJumpEnd;
		_view.EnableInput();
	}
	public void Uninitialize()
	{
		_view.Clicked -= OnRectClicked;
		_view.RotationEnd -= OnRotationEnd;
		_view.JumpEnd -= OnJumpEnd;
	}
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : UIView
{
	private Coroutine _showCo;

	public override void Initialize()
	{
		Bind<Image>("BackPanel");
		Bind<TextMeshProUGUI>("ScoreText");
		Bind<Button>("MenuButton");
		Bind<Button>("RestartButton");

		Get<Button>("MenuButton").onClick.AddListener(() => GameManager.Instance.ChangeScene(Enums.GAME_STATE.Menu));
		Get<Button>("RestartButton").onClick.AddListener(() => GameManager.Instance.ChangeScene(Enums.GAME_STATE.Game));
	}

	public override void Show()
	{
		base.Show();

		if (_showCo != null)
		{
			StopCoroutine(_showCo);
			_showCo = null;
		}

		_showCo = StartCoroutine(Fade(0.7f, 1f));

		Get<TextMeshProUGUI>("ScoreText").text = BattleManager.Instance.Score.ToString();
	}

	private IEnumerator Fade(float fadeValue, float fadeTime = 1f)
	{
		Image backPanel = Get<Image>("BackPanel");

		float time = 0;
		Color color = backPanel.color;

		while (time <= fadeTime)
		{
			time += Time.deltaTime;

			color.a = time / fadeTime * fadeValue;
			backPanel.color = color;

			yield return null;
		}
	}
}

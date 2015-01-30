using UnityEngine;
using System.Collections;

// ------------------------------------------------------
// 描述：游戏的全局控制类
// ------------------------------------------------------
public class GameController : MonoBehaviour {
	private int m_currAttackNumber;						// 当前攻击波次
	private float m_attackDuration;						// 当前波次需要持续的时间
	private float m_attackInterval;						// 当前波次间隔需要持续的时间
	private float m_currAttackDurationCount;			// 当前波次持续时间累计
	private float m_currAttackIntervalCount;			// 当前波次间隔时间累计

	/*
	 * 终止游戏，回到主菜单
	 */
	public void abortGame()
	{
		// TODO:
	}

	/*
	 * 开始游戏
	 */
	public void startGame()
	{
		// TODO:
	}

	/*
	 * 游戏结束，显示胜负、分数
	 */
	private void gameOver(bool isPlayerWin)
	{
		// TODO:
	}

	/*
	 * 开始下一波攻击
	 */
	private void startNextAttack()
	{
		// TODO:
	}

	/*
	 * 结束当前波次攻击
	 */
	private void endCurrAttack()
	{
		// TODO:
	}
}

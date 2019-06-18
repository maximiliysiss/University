package remoterclient.listener;

import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import remoterclient.forms.CustomContextMenu;

/**
 * Опеределение слушателя для событий мыши
 * @author
 *
 */
public class DoubleClickListener implements MouseListener {

	/**
	 * Обработчики
	 */
	DoubleClickHandler doubleClickHandler;
	CustomContextMenu onContextMenu;

	/**
	 * КОнструктор
	 * @param doubleClickHandler
	 * @param onContextMenu
	 */
	public DoubleClickListener(DoubleClickHandler doubleClickHandler, CustomContextMenu onContextMenu) {
		super();
		this.doubleClickHandler = doubleClickHandler;
		this.onContextMenu = onContextMenu;
	}

	/**
	 * Клик мышью
	 */
	@Override
	public void mouseClicked(MouseEvent e) {
		if (e.getClickCount() == 2 && doubleClickHandler != null) {
			doubleClickHandler.click();
		}
		if (e.getButton() == MouseEvent.BUTTON3 && onContextMenu != null) {
			onContextMenu.show(e.getComponent(), e.getX(), e.getY());
		}
	}

	@Override
	public void mousePressed(MouseEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public void mouseReleased(MouseEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public void mouseEntered(MouseEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public void mouseExited(MouseEvent e) {
		// TODO Auto-generated method stub

	}

}

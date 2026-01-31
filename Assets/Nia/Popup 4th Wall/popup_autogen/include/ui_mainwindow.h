/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 6.10.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenu>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QPushButton *pushButton;
    QPushButton *pushButton_2;
    QLabel *imageLabel;
    QMenuBar *menubar;
    QMenu *menuSINGLE_MOMS_IN_YOUR_AREA;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName("MainWindow");
        MainWindow->resize(800, 600);
        MainWindow->setUnifiedTitleAndToolBarOnMac(false);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName("centralwidget");
        pushButton = new QPushButton(centralwidget);
        pushButton->setObjectName("pushButton");
        pushButton->setGeometry(QRect(220, 460, 121, 51));
        pushButton->setAutoFillBackground(true);
        pushButton_2 = new QPushButton(centralwidget);
        pushButton_2->setObjectName("pushButton_2");
        pushButton_2->setGeometry(QRect(370, 460, 121, 51));
        pushButton_2->setAutoFillBackground(true);
        imageLabel = new QLabel(centralwidget);
        imageLabel->setObjectName("imageLabel");
        imageLabel->setGeometry(QRect(10, 10, 781, 581));
        imageLabel->setPixmap(QPixmap(QString::fromUtf8(":/images/Untitled8.png")));
        imageLabel->setScaledContents(true);
        MainWindow->setCentralWidget(centralwidget);
        imageLabel->raise();
        pushButton->raise();
        pushButton_2->raise();
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName("menubar");
        menubar->setGeometry(QRect(0, 0, 800, 25));
        menuSINGLE_MOMS_IN_YOUR_AREA = new QMenu(menubar);
        menuSINGLE_MOMS_IN_YOUR_AREA->setObjectName("menuSINGLE_MOMS_IN_YOUR_AREA");
        MainWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(MainWindow);
        statusbar->setObjectName("statusbar");
        MainWindow->setStatusBar(statusbar);

        menubar->addAction(menuSINGLE_MOMS_IN_YOUR_AREA->menuAction());

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "MainWindow", nullptr));
        pushButton->setText(QCoreApplication::translate("MainWindow", "YES TO ALL", nullptr));
        pushButton_2->setText(QCoreApplication::translate("MainWindow", "EXIT", nullptr));
        imageLabel->setText(QString());
        menuSINGLE_MOMS_IN_YOUR_AREA->setTitle(QCoreApplication::translate("MainWindow", "SINGLE MOMS IN YOUR AREA!!  ;))))))", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
